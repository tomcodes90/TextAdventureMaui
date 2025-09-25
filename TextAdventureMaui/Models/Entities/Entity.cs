using TextAdventureMaui.Models.Items.Weapons;

namespace TextAdventureMaui.Models;

public abstract class Entity(string? name, int maxHp, int attack)
{
    public string? Name { get; set; } = name;
    public int MaxHp { get; set; } = maxHp;
    public double CurrentHp { get; set; } = maxHp;
    public double BaseAttack { get; set; } = attack;

    public Weapon EquippedWeapon { get; set; }

    public bool IsAlive() => CurrentHp > 0;

    public void TakeDamage(double damage)
    {
        CurrentHp -= damage;
        if (CurrentHp < 0)
            CurrentHp = 0;
    }

    public double DealDamage()
    {
       return BaseAttack + EquippedWeapon.WeaponDamage();
    }
}
