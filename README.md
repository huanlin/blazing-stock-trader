# Blazing Stock Ttrader（熱血炒股）

A sample project for demonstrating .NET MAUI and Blazor programming. 

## 為什麼是 MAUI+Blazor？

這個程式會需要去爬其他網站的頁面來獲取股票相關資料，例如[台灣證券交易所](https://www.twse.com.tw/)。如果設計成單獨執行的 Blazor WASM app，那麼程式將受限於瀏覽器沙箱的安全限制而無法順利對任意網站發出 HTTP 請求。

如果設計成 hosted Blazor WASM app 或者 Blazor Server app，那就需要將 app 部署於有安裝 ASP.NET Core 的主機，而這通常需要付出額外的費用。對於一個展示用的免費 app 來說，我希望盡量不要付出額外的成本。

採用 MAUI+Blazor 混搭架構便可解決上述兩個主要考量，其優點包括：

- 避開瀏覽器沙箱的安全限制，可對任意第三方網站發出 HTTP 請求（爬網頁資料）。
- 省去後端主機的額外成本。
- 跨裝置平台，未來可能同時上架至 Microsoft Store、Google Play、和 Apple Store。
- 可存取裝置本身的資源與能力，例如本機磁碟、GPS 位置。（如果

