using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Plugin.Maui.Audio;
using TextAdventureMaui.Views;
using TextAdventureMaui.ViewModels;

namespace TextAdventureMaui;

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
                fonts.AddFont("EBGaramond-Regular.ttf", "GaramondRegular");
            });

        // Register services for DI
        builder.Services.AddSingleton(AudioManager.Current);

        // Register ViewModels and Pages
        builder.Services.AddSingleton<MainMenuViewModel>();
        builder.Services.AddSingleton<MainMenuPage>();

        builder.Services.AddTransient<BattlePage>();
        builder.Services.AddTransient<BattleViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
