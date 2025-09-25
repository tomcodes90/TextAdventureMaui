using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Services;
using TextAdventureMaui.ViewModels;
using TextAdventureMaui.Views;
using TextAdventureMaui.Factories;

public class BattleExecutor : IChallengeExecutor
{
    private readonly PlayerService _playerService;
    private readonly EnemyFactory _enemyFactory;
    private readonly RewardFactory _rewardFactory;
    private readonly ChallengeRewardService _rewardService;

    public BattleExecutor(
        PlayerService playerService,
        EnemyFactory enemyFactory,
        RewardFactory rewardFactory,
        ChallengeRewardService rewardService)
    {
        _playerService = playerService;
        _enemyFactory = enemyFactory;
        _rewardFactory = rewardFactory;
        _rewardService = rewardService;
    }

    public async Task<ChallengeResult> ExecuteAsync(Dictionary<string, object>? settings)
    {
        if (settings == null || !settings.TryGetValue("enemyName", out var enemyObj))
            throw new ArgumentException("Battle step requires an 'enemyName' in settings.");

        var enemyName = enemyObj?.ToString() ?? throw new ArgumentException("enemyName must be a string");

        var player = _playerService.CurrentPlayer;
        var enemy = _enemyFactory.CreateEnemyByName(enemyName);

        var vm = new BattleViewModel(player, enemy);
        var page = new BattlePage(vm);

        var tcs = new TaskCompletionSource<ChallengeResult>();

        vm.BattleEnded += (s, reward) =>
        {
            // Build the challenge result for this enemy
            var result = _rewardFactory.CreateReward(enemy.Name!, reward.Success);

            // Apply reward immediately (items, bonus, abilities)
            _rewardService.ApplyResult(player, result);

            // Return result to MissionRunner
            tcs.TrySetResult(result);
        };

        await Shell.Current.Navigation.PushAsync(page);
        var challengeResult = await tcs.Task;
        await Shell.Current.Navigation.PopAsync();

        return challengeResult;
    }
}
