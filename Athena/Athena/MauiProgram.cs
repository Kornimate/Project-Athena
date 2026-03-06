using Athena.Interfaces;
using Athena.Services;
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

            //Services
            builder.Services.AddSingleton<IWordCacheService, WordCacheService>();
            builder.Services.AddSingleton<IDataLoader, DataLoader>();
            builder.Services.AddSingleton<IDataWriter, DataWriter>();

            //Views
            builder.Services.AddSingleton<MainPage>(); // Register MainPage
            builder.Services.AddSingleton<ReadWordsPage>(); // Register ReadWordsPage
            builder.Services.AddSingleton<FlashCardsPage>(); // Register FlashCardsPage
            builder.Services.AddSingleton<AddWordPage>(); // Register AddWordPage
            builder.Services.AddSingleton<ListWordsPage>(); // Register ListWordsPage

            //ViewModels
            builder.Services.AddSingleton<MainViewModel>(); // Register MainViewModel
            builder.Services.AddSingleton<ReadWordsViewModel>(); // Register ReadWordsViewModel
            builder.Services.AddSingleton<FlashCardsViewModel>(); // Register FlashCardsViewModel
            builder.Services.AddSingleton<AddWordViewModel>(); // Register AddWordViewModel
            builder.Services.AddSingleton<ListWordsViewModel>(); // Register ListWordsViewModel

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // initialize the WordCacheService to load data at startup
            using(var scope = app.Services.CreateScope())
            {
                var wordCacheService = scope.ServiceProvider.GetRequiredService<IWordCacheService>();
                _ = wordCacheService.InitializeAsync();
            }

            return app;
        }
    }
}
