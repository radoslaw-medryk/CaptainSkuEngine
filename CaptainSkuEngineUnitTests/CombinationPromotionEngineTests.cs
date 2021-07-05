using System.Collections.Generic;
using CaptainSkuEngine.Engines.Combination;
using CaptainSkuEngine.Models;
using NUnit.Framework;

namespace CaptainSkuEngineUnitTests
{
    public class CombinationPromotionEngineTests
    {
        private static readonly Sku TestSkuA = new Sku
        {
            Id = "A",
            Price = 50
        };
        
        private static readonly Sku TestSkuB = new Sku
        {
            Id = "B",
            Price = 30
        };
        
        private static readonly Sku TestSkuC = new Sku
        {
            Id = "C",
            Price = 20
        };
        
        private static readonly Sku TestSkuD = new Sku
        {
            Id = "D",
            Price = 15
        };
        
        [Test]
        public void NoPromotionApplied()
        {
            var engine = CreateTestEngine();

            var result = engine.ApplyPromotion(new List<SkuWithCount>
            {
                new SkuWithCount
                {
                    Sku = TestSkuA,
                    Count = 1
                },
                new SkuWithCount
                {
                    Sku = TestSkuB,
                    Count = 1
                },
                new SkuWithCount
                {
                    Sku = TestSkuC,
                    Count = 1
                },
            });
            
            Helpers.AssertJsonEqual(result, 
                new PromotionResult
                {
                    TotalPrice = 100,
                    PromotionalGroups = new List<PricedGroup>(),
                    OmittedEntries = new []
                    {
                        new SkuWithCount
                        {
                            Sku = TestSkuA,
                            Count = 1
                        },
                        new SkuWithCount
                        {
                            Sku = TestSkuB,
                            Count = 1
                        },
                        new SkuWithCount
                        {
                            Sku = TestSkuC,
                            Count = 1
                        },
                    }
                });
        }

        private PromotionEngine CreateTestEngine()
        {
            return new PromotionEngine(new List<PricedGroup>
            {
                new PricedGroup
                {
                    Entries = new[]{ new SkuWithCount { Sku = TestSkuA, Count = 3 } },
                    TotalPrice = 130
                },
                new PricedGroup
                {
                    Entries = new[]{ new SkuWithCount { Sku = TestSkuB, Count = 2 } },
                    TotalPrice = 45
                },
                new PricedGroup
                {
                    Entries = new[]
                    {
                        new SkuWithCount { Sku = TestSkuC, Count = 1 },
                        new SkuWithCount { Sku = TestSkuD, Count = 1 }
                    },
                    TotalPrice = 30
                }
            });
        }
    }
}