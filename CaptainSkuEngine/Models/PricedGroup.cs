using System.Collections.Generic;

namespace CaptainSkuEngine.Models
{
    public class PricedGroup
    {
        public List<BasketEntry> Entries { get; set; }
        public decimal TotalPrice { get; set; }
    }
}