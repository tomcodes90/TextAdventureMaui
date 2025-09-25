using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using TextAdventureMaui.Factories;
using TextAdventureMaui.Models.Missions;
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
        builder.Services.AddSingleton<MainHallService>();
        builder.Services.AddSingleton<NavigationService>();
        builder.Services.AddSingleton<ConversationService>();
        builder.Services.AddSingleton<ChallengeRewardService>();
        builder.Services.AddSingleton<AbilityFactory>();
        builder.Services.AddSingleton<ItemFactory>();
        builder.Services.AddSingleton<DobbleService>();
        builder.Services.AddSingleton<PuzzleService>();

        // Enemy provider
        builder.Services.AddSingleton<IEnemyProvider, EnemyProvider>();

        // ----------------------------------------------------
        // Challenge Executors
        // ----------------------------------------------------
        builder.Services.AddTransient<BattleExecutor>();
        builder.Services.AddTransient<DobbleExecutor>();
        builder.Services.AddTransient<PuzzleExecutor>();

        // Mission Runner
        builder.Services.AddSingleton<MissionRunner>(sp =>
        {
            var conv = sp.GetRequiredService<ConversationService>();

            var executors = new Dictionary<ChallengeType, IChallengeExecutor>
            {
                { ChallengeType.Battle, sp.GetRequiredService<BattleExecutor>() },
                { ChallengeType.Dobble, sp.GetRequiredService<DobbleExecutor>() },
                { ChallengeType.Puzzle, sp.GetRequiredService<PuzzleExecutor>() }
            };

            return new MissionRunner(
                conv,
                executors,
                sp.GetRequiredService<ChallengeRewardService>(),
                sp.GetRequiredService<PlayerService>(),
                sp.GetRequiredService<AbilityFactory>(),
                sp
            );
        });

        // ----------------------------------------------------
        // ViewModels
        // ----------------------------------------------------
        builder.Services.AddTransient<MainMenuViewModel>();
        builder.Services.AddTransient<MainHallViewModel>();
        builder.Services.AddTransient<DobbleViewModel>();

        // ----------------------------------------------------
        // Pages
        // ----------------------------------------------------
        builder.Services.AddTransient<MainMenuPage>();
        builder.Services.AddTransient<MainHallPage>();
        builder.Services.AddTransient<BattlePage>();
        builder.Services.AddTransient<DobblePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
