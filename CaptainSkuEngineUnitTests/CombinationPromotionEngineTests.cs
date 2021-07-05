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
            
            Helpers.AssertJsonEqual( 
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
                }, result);
        }
        
        [Test]
        public void AppliesSingleSkuPromotions()
        {
            var engine = CreateTestEngine();

            var result = engine.ApplyPromotion(new List<SkuWithCount>
            {
                new SkuWithCount
                {
                    Sku = TestSkuA,
                    Count = 5
                },
                new SkuWithCount
                {
                    Sku = TestSkuB,
                    Count = 5
                },
                new SkuWithCount
                {
                    Sku = TestSkuC,
                    Count = 1
                },
            });
            
            Helpers.AssertJsonEqual( 
                new PromotionResult
                {
                    TotalPrice = 370,
                    PromotionalGroups = new List<PricedGroup>
                    {
                        new PricedGroup
                        {
                            TotalPrice = 130,
                            Entries = new List<SkuWithCount>
                            {
                                new SkuWithCount
                                {
                                    Sku = TestSkuA,
                                    Count = 3
                                }
                            }
                        },
                        new PricedGroup
                        {
                            TotalPrice = 45,
                            Entries = new List<SkuWithCount>
                            {
                                new SkuWithCount
                                {
                                    Sku = TestSkuB,
                                    Count = 2
                                }
                            }
                        },
                        new PricedGroup
                        {
                            TotalPrice = 45,
                            Entries = new List<SkuWithCount>
                            {
                                new SkuWithCount
                                {
                                    Sku = TestSkuB,
                                    Count = 2
                                }
                            }
                        }
                    },
                    OmittedEntries = new []
                    {
                        new SkuWithCount
                        {
                            Sku = TestSkuA,
                            Count = 2
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
                }, result);
        }
        
        [Test]
        public void AppliesMultiSkuPromotions()
        {
            var engine = CreateTestEngine();

            var result = engine.ApplyPromotion(new List<SkuWithCount>
            {
                new SkuWithCount
                {
                    Sku = TestSkuA,
                    Count = 3
                },
                new SkuWithCount
                {
                    Sku = TestSkuB,
                    Count = 5
                },
                new SkuWithCount
                {
                    Sku = TestSkuC,
                    Count = 1
                },
                new SkuWithCount
                {
                    Sku = TestSkuD,
                    Count = 1
                },
            });
            
            Helpers.AssertJsonEqual( 
                new PromotionResult
                {
                    TotalPrice = 280,
                    PromotionalGroups = new List<PricedGroup>
                    {
                        new PricedGroup
                        {
                            TotalPrice = 130,
                            Entries = new List<SkuWithCount>
                            {
                                new SkuWithCount
                                {
                                    Sku = TestSkuA,
                                    Count = 3
                                }
                            }
                        },
                        new PricedGroup
                        {
                            TotalPrice = 45,
                            Entries = new List<SkuWithCount>
                            {
                                new SkuWithCount
                                {
                                    Sku = TestSkuB,
                                    Count = 2
                                }
                            }
                        },
                        new PricedGroup
                        {
                            TotalPrice = 45,
                            Entries = new List<SkuWithCount>
                            {
                                new SkuWithCount
                                {
                                    Sku = TestSkuB,
                                    Count = 2
                                }
                            }
                        },
                        new PricedGroup
                        {
                            TotalPrice = 30,
                            Entries = new List<SkuWithCount>
                            {
                                new SkuWithCount
                                {
                                    Sku = TestSkuC,
                                    Count = 1
                                },
                                new SkuWithCount
                                {
                                    Sku = TestSkuD,
                                    Count = 1
                                }
                            }
                        }
                    },
                    OmittedEntries = new []
                    {
                        new SkuWithCount
                        {
                            Sku = TestSkuB,
                            Count = 1
                        },
                    }
                }, result);
        }
        
        [Test]
        public void SkipsEmptyRules()
        {
            var engine = new PromotionEngine(new List<PricedGroup>
            {
                new PricedGroup
                {
                    Entries = new SkuWithCount[]{ },
                    TotalPrice = 45
                },
            });

            var result = engine.ApplyPromotion(new List<SkuWithCount>
            {
                new SkuWithCount
                {
                    Sku = TestSkuA,
                    Count = 5
                },
                new SkuWithCount
                {
                    Sku = TestSkuB,
                    Count = 3
                },
                new SkuWithCount
                {
                    Sku = TestSkuC,
                    Count = 1
                },
            });
            
            Helpers.AssertJsonEqual( 
                new PromotionResult
                {
                    TotalPrice = 360,
                    PromotionalGroups = new List<PricedGroup>(),
                    OmittedEntries = new []
                    {
                        new SkuWithCount
                        {
                            Sku = TestSkuA,
                            Count = 5
                        },
                        new SkuWithCount
                        {
                            Sku = TestSkuB,
                            Count = 3
                        },
                        new SkuWithCount
                        {
                            Sku = TestSkuC,
                            Count = 1
                        },
                    }
                }, result);
        }
        
        [Test]
        public void AppliesRulesInDefinedOrder()
        {
            var engine = new PromotionEngine(new List<PricedGroup>
            {
                new PricedGroup
                {
                    Entries = new []{ new SkuWithCount { Sku = TestSkuA, Count = 3} },
                    TotalPrice = 130
                },
                new PricedGroup
                {
                    Entries = new []{ new SkuWithCount { Sku = TestSkuA, Count = 3} },
                    TotalPrice = 9999
                },
            });

            var result = engine.ApplyPromotion(new List<SkuWithCount>
            {
                new SkuWithCount
                {
                    Sku = TestSkuA,
                    Count = 3
                },
            });
            
            Helpers.AssertJsonEqual( 
                new PromotionResult
                {
                    TotalPrice = 130,
                    PromotionalGroups = new List<PricedGroup>
                    {
                        new PricedGroup
                        {
                            Entries = new [] { new SkuWithCount {Sku = TestSkuA, Count = 3} },
                            TotalPrice = 130
                        }
                    },
                    OmittedEntries = new SkuWithCount[]{ }
                }, result);
        }
        
        [Test]
        public void RulesDontOverlapTheSameSku()
        {
            var engine = new PromotionEngine(new List<PricedGroup>
            {
                new PricedGroup
                {
                    Entries = new []{ new SkuWithCount { Sku = TestSkuC, Count = 1}, new SkuWithCount { Sku = TestSkuD, Count = 1} },
                    TotalPrice = 100
                },
                new PricedGroup
                {
                    Entries = new []{ new SkuWithCount { Sku = TestSkuB, Count = 1}, new SkuWithCount { Sku = TestSkuC, Count = 1} },
                    TotalPrice = 9999
                },
            });

            var result = engine.ApplyPromotion(new List<SkuWithCount>
            {
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
                new SkuWithCount
                {
                    Sku = TestSkuD,
                    Count = 1
                },
            });
            
            Helpers.AssertJsonEqual( 
                new PromotionResult
                {
                    TotalPrice = 130,
                    PromotionalGroups = new List<PricedGroup>
                    {
                        new PricedGroup
                        {
                            Entries = new []{ new SkuWithCount { Sku = TestSkuC, Count = 1}, new SkuWithCount { Sku = TestSkuD, Count = 1} },
                            TotalPrice = 100
                        },
                    },
                    OmittedEntries = new []
                    {
                        new SkuWithCount
                        {
                            Sku = TestSkuB,
                            Count = 1
                        }
                    }
                }, result);
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