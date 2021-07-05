using System;
using System.Collections.Generic;
using CaptainSkuEngine.Models;

namespace CaptainSkuEngine.Engines.Combination
{
    public class CombinationPromotionEngine : IPromotionEngine
    {
        private readonly ICollection<PricedGroup> _combinations;
        
        public CombinationPromotionEngine(ICollection<PricedGroup> combinations)
        {
            _combinations = combinations;
        }
        
        public ICollection<PromotionResult> ApplyPromotion(ICollection<SkuWithCount> entries)
        {
            throw new NotImplementedException();
        }
    }
}