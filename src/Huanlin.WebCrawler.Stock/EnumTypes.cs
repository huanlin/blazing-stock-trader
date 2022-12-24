using System;
using System.Collections.Generic;
using System.Text;

namespace Huanlin.WebCrawler.Stock;

public enum SymbolQueryType
{
    Stock,   // 上市和上櫃股票
    Etf      // 上市和上櫃 ETF   

}

public enum DailyPriceQueryType
{
    All,   // 上市和上櫃股票（排除牛熊權證）
    Etf,   // 上市和上櫃 ETF
    MarketIndex // 市場指數
}
