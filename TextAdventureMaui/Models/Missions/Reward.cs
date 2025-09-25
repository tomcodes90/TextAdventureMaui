namespace TextAdventureMaui.Models.Missions;

public class Reward
{
    public int Coins { get; set; }
    public List<string> Loot { get; set; } = new();
    public bool UnlockAbility { get; set; }
}
