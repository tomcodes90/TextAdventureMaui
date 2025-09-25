namespace TextAdventureMaui.Models.Items.Weapons;

public abstract class Weapon(string name, string description, double damage, double critChance, double critMultiplier = 2.0)
    : Item(name, description, true)
{
    public double Damage { get; protected set; } = damage;
    public double CritChance { get; protected set; } = critChance;
    public double CritMultiplier { get; protected set; } = critMultiplier;

    public string Icon { get; protected set; } = "default_weapon.png";
    public string DisplayName { get; protected set; } = name;

    protected readonly Random Rng = new();

    public double RollDamage()
    {
        bool isCrit = Rng.NextDouble() < CritChance;
        return isCrit ? Damage * CritMultiplier : Damage;
    }

    public abstract double WeaponDamage();
}