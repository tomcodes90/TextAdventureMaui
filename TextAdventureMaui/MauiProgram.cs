
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using TextAdventureMaui.Services;
using TextAdventureMaui.ViewModels;
using TextAdventureMaui.Views;

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
                fonts.AddFont("DungeonFont.ttf", "DungeonFont");
            });

        // ----------------------------------------------------
        // Core / Third-party services
        // ----------------------------------------------------
        builder.Services.AddSingleton<IAudioManager>(AudioManager.Current);

        // ----------------------------------------------------
        // Game Services
        // ----------------------------------------------------
        builder.Services.AddSingleton<PlayerService>();
        builder.Services.AddSingleton<DialogueService>();
        builder.Services.AddSingleton<CharacterCreationService>();
        builder.Services.AddSingleton<AbilityLoader>();

        // ----------------------------------------------------
        // ViewModels
        // ----------------------------------------------------
        builder.Services.AddTransient<MainMenuViewModel>();
        builder.Services.AddTransient<MainHallViewModel>();

        // ----------------------------------------------------
        // Pages
        // ----------------------------------------------------
        builder.Services.AddTransient<MainMenuPage>();
        builder.Services.AddTransient<MainHallPage>();
        builder.Services.AddTransient<BattlePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}