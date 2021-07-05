using System.Collections.Generic;
using CaptainSkuEngine.Models;

namespace CaptainSkuEngine.Engines
{
    public interface IPromotionEngine
    {
        PromotionResult ApplyPromotion(ICollection<BasketEntry> entries);
    }
}