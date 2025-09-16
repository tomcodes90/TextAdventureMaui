using TextAdventureMaui.Models.Items;

namespace TextAdventureMaui.Models;

public class Entity(string? name, int maxHp, int attack)
{
    public string? Name { get; set; } = name;
    public int MaxHp { get; set; } = maxHp;
    public int CurrentHp { get; set; } = maxHp;
    public Weapon EquippedWeapon { get; set; }
    public bool IsAlive() => CurrentHp > 0;

    public virtual void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        if (CurrentHp < 0)
            CurrentHp = 0;
    }
    public virtual int DealDamage()
    {
        return EquippedWeapon.Damage;
    }
}