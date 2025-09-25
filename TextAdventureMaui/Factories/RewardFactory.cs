using System.Text.Json;
using TextAdventureMaui.Models.Missions;

namespace TextAdventureMaui.Services;

public class RewardFactory
{
    private readonly Dictionary<int, RewardData> _rewardData;

    public RewardFactory()
    {
        // Carichiamo il file dal pacchetto
        using var stream = FileSystem.OpenAppPackageFileAsync("Data/rewards.json").Result;
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        var rewards = JsonSerializer.Deserialize<List<RewardData>>(json)!;
        _rewardData = rewards.ToDictionary(r => r.EnemyId);
    }

    public ChallengeReward CreateReward(int enemyId, bool playerWon)
    {
        if (!_rewardData.TryGetValue(enemyId, out var data))
            throw new ArgumentException($"Reward for enemy id {enemyId} not found.");

        return new ChallengeReward(
            playerWon,
            data.CanChooseUpgrade && playerWon,
            data.NewAbilityUnlockedId,
            data.Loot
        );
    }

    private class RewardData
    {
        public int EnemyId { get; set; }
        public bool CanChooseUpgrade { get; set; }
        public int? NewAbilityUnlockedId { get; set; }
        public List<string> Loot { get; set; } = new();
    }
}