using Athena.ViewModels;
using Athena.Views;
using Microsoft.Extensions.Logging;

namespace Athena
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Views
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<ReadWordsPage>();
            builder.Services.AddSingleton<FlashCardsPage>();

            //ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<ReadWordsViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
