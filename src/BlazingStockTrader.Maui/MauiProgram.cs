using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using BlazingStockTrader.WebUI.Data;
using Huanlin.WebCrawler.Stock;

namespace BlazingStockTrader.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<TwseWebClient>();
            builder.Services.AddMudServices();

            return builder.Build();
        }
    }
}