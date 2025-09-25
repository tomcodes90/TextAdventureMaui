using System.Text.Json;
using TextAdventureMaui.Models;

namespace TextAdventureMaui.Factories;

public class RewardFactory
{
    private readonly Dictionary<string, ChallengeResult> _rewards = new();

    public RewardFactory()
    {
        LoadRewardsAsync().Wait();
    }

    private async Task LoadRewardsAsync()
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("rewards.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            var rewards = JsonSerializer.Deserialize<List<ChallengeResult>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (rewards != null)
            {
                foreach (var reward in rewards)
                {
                    if (!string.IsNullOrWhiteSpace(reward.EnemyName))
                        _rewards[reward.EnemyName] = reward;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Failed to load rewards.json: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a ChallengeResult for a given enemy and outcome.
    /// </summary>
    public ChallengeResult CreateReward(string enemyName, bool success)
    {
        if (_rewards.TryGetValue(enemyName, out var reward))
        {
            return new ChallengeResult(
                success,
                reward.EarnedItems,
                reward.Bonus,
                reward.UnlockedAbilityId
            )
            {
                EnemyName = enemyName
            };
        }

        throw new KeyNotFoundException($"No reward defined for enemy '{enemyName}' in rewards.json.");
    }
}