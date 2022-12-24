namespace Huanlin.WebCrawler.Stock;


internal static class MarketType
{
    public static string StockExchange = "1";   // 集中市場
    public static string OverTheCounter = "2";  // 上櫃
}

internal static class StockExchangeMarket
{
    public static class IssueType
    {
        public const string Stock = "1";        // 上市股票
        public const string SpecialStock = "A"; // 特別股
        public const string Etf = "I";          // 上市 ETF
    }
}

internal static class OverTheCounterMarket
{
    public static class IssueType
    {
        public const string Stock = "4";    // 上櫃股票
        public const string Etf = "3";      // 上櫃 ETF
    }
}
