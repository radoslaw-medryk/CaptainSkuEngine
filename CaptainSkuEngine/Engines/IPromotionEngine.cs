using System.Collections.Generic;
using CaptainSkuEngine.Models;

namespace CaptainSkuEngine.Engines
{
    public interface IPromotionEngine
    {
        ICollection<PromotionResult> ApplyPromotion(ICollection<SkuWithCount> entries);
    }
}