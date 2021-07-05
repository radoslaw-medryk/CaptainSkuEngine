using System.Collections.Generic;

namespace CaptainSkuEngine.Models
{
    public class PricedGroup
    {
        public ICollection<SkuWithCount> Entries { get; set; }
        public decimal TotalPrice { get; set; }
    }
}