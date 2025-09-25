namespace TextAdventureMaui.Models;

public class ChallengeResult : IHasEnemyName
{
    public string EnemyName { get; set; } = "";   // 👈 now required by the interface

    /// <summary>
    /// True if the player successfully completed the challenge.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// List of item IDs or names earned.
    /// </summary>
    public List<string> EarnedItems { get; set; } = new();

    /// <summary>
    /// Optional stat bonus (e.g. "Damage", "Health").
    /// </summary>
    public string? Bonus { get; set; }

    /// <summary>
    /// Optional ability unlocked (by ID).
    /// </summary>
    public int? UnlockedAbilityId { get; set; }

    public ChallengeResult() { } // Required for JSON deserialization

    public ChallengeResult(
        bool success,
        List<string>? earnedItems = null,
        string? bonus = null,
        int? unlockedAbilityId = null)
    {
        Success = success;
        EarnedItems = earnedItems ?? new List<string>();
        Bonus = bonus;
        UnlockedAbilityId = unlockedAbilityId;
    }
}