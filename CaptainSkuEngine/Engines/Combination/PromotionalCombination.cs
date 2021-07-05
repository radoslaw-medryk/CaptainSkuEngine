using CaptainSkuEngine.Models;

namespace CaptainSkuEngine.Engines.Combination
{
    public class PromotionalCombination
    {
        public Sku Sku { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}