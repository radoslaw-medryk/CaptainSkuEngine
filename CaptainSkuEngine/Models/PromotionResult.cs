using System.Collections.Generic;

namespace CaptainSkuEngine.Models
{
    public class PromotionResult
    {
        public ICollection<PricedGroup> PromotionalGroups { get; set; }
        public ICollection<SkuWithCount> OmittedEntries { get; set; }
        public decimal TotalPrice { get; set; }
    }
}