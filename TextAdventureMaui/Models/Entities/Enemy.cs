using TextAdventureMaui.Models.Entities;

namespace TextAdventureMaui.Models.Entities;

public class Enemy(string? name, int maxHp, int attack) : Entity(name, maxHp, attack)
{
    private string v1;
    private int v2;


    public EnemyType EnemyType { get; set; }
}