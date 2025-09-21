using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Plugin.Maui.Audio;
using TextAdventureMaui.Views;
using TextAdventureMaui.ViewModels;
using TextAdventureMaui.Models.Missions;
using TextAdventureMaui.Services;

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
        builder.Services.AddSingleton<DialogueService>();
        // Register MainHall (with default doors + npcs)
        builder.Services.AddSingleton<MainHall>();

        // Register ViewModel
        builder.Services.AddTransient<MainHallViewModel>();

        // Register Page
        builder.Services.AddTransient<MainHallPage>();

        builder.Services.AddTransient<BattlePage>();
     

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
