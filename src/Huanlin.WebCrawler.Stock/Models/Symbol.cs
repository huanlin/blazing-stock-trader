using System;

namespace Huanlin.WebCrwaler.Stock.Models
{
    public class Symbol
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? YahooCodeSuffix { get; set; }
        public string? TwseRealTimeId { get; set; }
        public string? ProductType { get; set; }
        public string? MarketType { get; set; }
        public string? IndustryType { get; set; }
        public string? StartTime { get; set; }
        public double ShareCapital_Yi { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime UpdateTime { get; set; }

        public bool IsFinancialStock
        {
            get
            {
                return IndustryType == null ? false : IndustryType.Contains("金融");
            }
        }

        public bool IsEtf
        {
            get
            {
                return ProductType == "ETF";
            }
        }
    }
}
