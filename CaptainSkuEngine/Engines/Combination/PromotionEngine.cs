using System;
using System.Collections.Generic;
using System.Linq;
using CaptainSkuEngine.Models;

namespace CaptainSkuEngine.Engines.Combination
{
    public class PromotionEngine : IPromotionEngine
    {
        private readonly ICollection<PricedGroup> _rules;
        
        public PromotionEngine(ICollection<PricedGroup> rules)
        {
            _rules = rules;
        }
        
        public PromotionResult ApplyPromotion(ICollection<SkuWithCount> entries)
        {
            var promotionalGroups = new List<PricedGroup>();
            var entriesLeft = entries.ToList();

            foreach (var rule in _rules)
            {
                var ruleResult = ApplyRule(entriesLeft, rule);
                
                promotionalGroups.AddRange(ruleResult.PromotionalGroups);
                entriesLeft.AddRange(ruleResult.OmittedEntries);
            }

            var totalPrice = promotionalGroups.Sum(q => q.TotalPrice) + entriesLeft.Sum(q => q.Sku.Price * q.Count);
            
            return new PromotionResult
            {
                PromotionalGroups = promotionalGroups,
                OmittedEntries = entriesLeft,
                TotalPrice = totalPrice,
            };
        }

        private PromotionResult ApplyRule(ICollection<SkuWithCount> entries, PricedGroup rule)
        {
            int? applicationCount = null;

            foreach (var ruleEntry in rule.Entries)
            {
                var totalSkuCount = entries.Sum(q => q.Sku.Id == ruleEntry.Sku.Id ? q.Count : 0);
                var ruleRequiredSkuCount = ruleEntry.Count;

                var entryApplicationCount = (int)Math.Floor((double)totalSkuCount / ruleRequiredSkuCount);

                applicationCount = Math.Min(entryApplicationCount, applicationCount ?? Int32.MaxValue);
            }

            return ApplyRuleNumberOfTimes(entries, rule, applicationCount ?? 0);
        }

        private PromotionResult ApplyRuleNumberOfTimes(ICollection<SkuWithCount> entries, PricedGroup rule, int numberOfTimes)
        {
            var omittedEntries = new List<SkuWithCount>();

            foreach (var entry in entries)
            {
                var ruleEntry = rule.Entries.SingleOrDefault(q => q.Sku.Id == entry.Sku.Id);
                var ruleCount = (ruleEntry?.Count ?? 0) * numberOfTimes;
                var countLeft = entry.Count - ruleCount;

                if (countLeft > 0)
                {
                    omittedEntries.Add(new SkuWithCount
                    {
                        Sku = entry.Sku,
                        Count = countLeft,
                    });
                }
            }

            var promotionalGroups = Enumerable.Range(0, numberOfTimes).Select(_ => new PricedGroup
            {
                Entries = rule.Entries.ToArray(),
                TotalPrice = rule.TotalPrice,
            }).ToArray();

            var totalPrice = omittedEntries.Sum(q => q.Sku.Price * q.Count) + promotionalGroups.Sum(q => q.TotalPrice);
            
            return new PromotionResult
            {
                OmittedEntries = omittedEntries,
                PromotionalGroups = promotionalGroups,
                TotalPrice = totalPrice
            };
        }
    }
}