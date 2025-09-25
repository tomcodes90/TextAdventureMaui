namespace TextAdventureMaui.Models;

public class BattleResult
{
    public bool PlayerWon { get; }
    public int Coins { get; }
    public List<string> Loot { get; }

    public BattleResult(bool playerWon, int coins, List<string>? loot = null)
    {
        PlayerWon = playerWon;
        Coins = coins;
        Loot = loot ?? new List<string>();
    }
}