using System.Collections.Generic;
using System.Linq;
using CaptainSkuEngine.Engines;
using CaptainSkuEngine.Models;

namespace CaptainSkuEngine.Services
{
    public class PromotionService
    {
        private readonly ICollection<IPromotionEngine> _engines;
        
        public PromotionService(ICollection<IPromotionEngine> engines)
        {
            _engines = engines;
        }
        
        public PromotionResult ApplyPromotion(ICollection<SkuWithCount> entries)
        {
            var promotionalGroups = new List<PricedGroup>();
            var entriesLeft = entries.ToList();

            foreach (var engine in _engines)
            {
                var engineResult = engine.ApplyPromotion(entriesLeft);
            
                promotionalGroups.AddRange(engineResult.PromotionalGroups);
                entriesLeft = engineResult.OmittedEntries.ToList();
            }

            var totalPrice = promotionalGroups.Sum(q => q.TotalPrice) + entriesLeft.Sum(q => q.Sku.Price * q.Count);
        
            return new PromotionResult
            {
                PromotionalGroups = promotionalGroups,
                OmittedEntries = entriesLeft,
                TotalPrice = totalPrice,
            };
        }
    }
}