namespace TextAdventureMaui.Models.Missions;

public class ChallengeReward
{
    public bool PlayerWon { get; }
    public bool CanChooseUpgrade { get; }
    public int? NewAbilityUnlockedId { get; }
    public List<string> Loot { get; }

    public ChallengeReward(
        bool playerWon,
        bool canChooseUpgrade,
        int? newAbilityUnlockedId = null,
        List<string>? loot = null)
    {
        PlayerWon = playerWon;
        CanChooseUpgrade = canChooseUpgrade;
        NewAbilityUnlockedId = newAbilityUnlockedId;
        Loot = loot ?? new List<string>();
    }
}