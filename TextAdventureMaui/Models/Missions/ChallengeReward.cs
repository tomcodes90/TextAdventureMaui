namespace TextAdventureMaui.Models.Missions;

public class ChallengeReward
{
    public bool PlayerWon { get; }
    public string? NewAbilityUnlocked { get; }
    public bool CanChooseUpgrade { get; }

    public ChallengeReward(bool playerWon, string? newAbilityUnlocked = null, bool canChooseUpgrade = false)
    {
        PlayerWon = playerWon;
        NewAbilityUnlocked = newAbilityUnlocked;
        CanChooseUpgrade = canChooseUpgrade;
    }
}