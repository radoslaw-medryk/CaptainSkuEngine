using System.Collections.Generic;

namespace CaptainSkuEngine.Models
{
    public class PromotionResult
    {
        public ICollection<PricedGroup> PromotionalGroups { get; set; }
        public ICollection<BasketEntry> OmittedEntries { get; set; }
    }
}