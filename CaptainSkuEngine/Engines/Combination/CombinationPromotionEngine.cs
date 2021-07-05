using System;
using System.Collections.Generic;
using CaptainSkuEngine.Models;

namespace CaptainSkuEngine.Engines.Combination
{
    public class CombinationPromotionEngine : IPromotionEngine
    {
        private readonly ICollection<PromotionalCombination> _combinations;
        
        public CombinationPromotionEngine(ICollection<PromotionalCombination> combinations)
        {
            _combinations = combinations;
        }
        
        public PromotionResult ApplyPromotion(ICollection<BasketEntry> entries)
        {
            throw new NotImplementedException();
        }
    }
}