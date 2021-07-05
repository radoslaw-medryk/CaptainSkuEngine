using System.Collections.Generic;
using CaptainSkuEngine.Models;

namespace CaptainSkuEngine.Services
{
    public interface IPromotionService
    {
        PromotionResult ApplyPromotion(ICollection<SkuWithCount> entries);
    }
}