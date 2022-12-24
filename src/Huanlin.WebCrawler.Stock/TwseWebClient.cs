using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using Huanlin.WebCrawler.Stock.Helpers;
using Huanlin.WebCrwaler.Stock.Models;

namespace Huanlin.WebCrawler.Stock
{
    public class TwseServerBusyException : Exception
    {
        public TwseServerBusyException(string message) : base(message)
        {
        }
    }

    public class TwseWebClient
    {
        private HttpClient _httpClient;

        public TwseWebClient()
        {
            _httpClient = HttpClientHelper.CreateHttpClient(string.Empty, "www.twse.com.tw");
        }

       private async Task<List<Symbol>> ParseSymbolFromHtmlAsync(string htmlString)
        {
            var htmlParser = new HtmlParser();
            var htmlDoc = await htmlParser.ParseDocumentAsync(htmlString);
            var rows = htmlDoc.QuerySelectorAll("tr");

            if (rows.Length < 1)
            {
                throw new Exception("無法剖析網頁：找不到所需之表格元素!");
            }

            var symbols = new List<Symbol>();
            for (int i = 0; i < rows.Length; i++)
            {
                if (i == 0) continue;   // 略過標題列

                var cells = rows[i].QuerySelectorAll("td");

                var symbol = new Symbol
                {
                    Id = cells[2].TextContent.Trim(),
                    Name = cells[3].TextContent.Trim(),
                    MarketType = cells[4].TextContent.Trim(),
                    ProductType = cells[5].TextContent.Trim(),
                    IndustryType = cells[6].TextContent.Trim(),
                    StartTime = cells[7].TextContent.Trim()
                };

                symbols.Add(symbol);
            }
            return symbols;
        }

        // Short methods for getting stock and ETF symbols.
        public async Task<List<Symbol>> GetStockSymbolsAsync() => await GetSymbolsAsync(SymbolQueryType.Stock);
        public async Task<List<Symbol>> GetEtfSymbolsAsync() => await GetSymbolsAsync(SymbolQueryType.Etf);


        /// <summary>
        /// 從證交所取得所有上市和上櫃的股票名單。注意：不含興櫃。
        /// 網頁查詢，單筆：https://www.twse.com.tw/zh/page/products/stock-code1.html
        /// 網頁查詢，多筆：https://isin.twse.com.tw/isin/class_i.jsp?kind=1
        /// </summary>
        /// <returns></returns>
        public async Task<List<Symbol>> GetSymbolsAsync(SymbolQueryType requestType)
        {
            string urlTemplate = "https://isin.twse.com.tw/isin/class_main.jsp?owncode=&stockname=&isincode=&market={0}&issuetype={1}&industry_code=&Page=1";
            string urlExchangeMarket = null;
            string urlOtcMarket = null;

            switch (requestType)
            {
                case SymbolQueryType.Stock:
                    urlExchangeMarket = string.Format(urlTemplate, MarketType.StockExchange, StockExchangeMarket.IssueType.Stock);
                    urlOtcMarket = string.Format(urlTemplate, MarketType.OverTheCounter, OverTheCounterMarket.IssueType.Stock);
                    break;
                case SymbolQueryType.Etf:
                    urlExchangeMarket = string.Format(urlTemplate, MarketType.StockExchange, StockExchangeMarket.IssueType.Etf);
                    urlOtcMarket = string.Format(urlTemplate, MarketType.OverTheCounter, OverTheCounterMarket.IssueType.Etf);
                    break;
            }

            var bytes = await _httpClient.GetByteArrayAsync(urlExchangeMarket);
            var htmlString = Encoding.GetEncoding(950).GetString(bytes, 0, bytes.Length - 1);
            var stocks = await ParseSymbolFromHtmlAsync(htmlString);

            await Task.Delay(2000); // 延遲一下，避免對方主機封鎖 IP。

            bytes = await _httpClient.GetByteArrayAsync(urlOtcMarket);
            htmlString = Encoding.GetEncoding(950).GetString(bytes, 0, bytes.Length - 1);
            var octMarketStocks = await ParseSymbolFromHtmlAsync(htmlString);

            stocks.AddRange(octMarketStocks);

            return stocks;
        }
    }
}
