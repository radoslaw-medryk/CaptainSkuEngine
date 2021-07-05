using System.Collections.Generic;

namespace CaptainSkuEngine.Models
{
    public class PricedGroup
    {
        public ICollection<BasketEntry> Entries { get; set; }
        public decimal TotalPrice { get; set; }
    }
}