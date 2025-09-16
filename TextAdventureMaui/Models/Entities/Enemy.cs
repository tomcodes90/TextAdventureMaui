using TextAdventureMaui.Models.Entities;

namespace TextAdventureMaui.Models;

public class Enemy(string? name, int maxHp, int attack) : Entity(name, maxHp, attack)
{
    public EnemyType EnemyType { get; set; }
}