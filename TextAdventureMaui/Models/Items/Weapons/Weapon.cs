namespace TextAdventureMaui.Models.Items.Weapons;

public abstract class Weapon(string name, string description, double damage, double critChance, double critMultiplier = 2.0)
    : Item(name, description, true)
{
    public double Damage { get; protected set; } = damage;
    public double CritChance { get; protected set; } = critChance;
    public double CritMultiplier { get; protected set; } = critMultiplier;

    protected readonly Random Rng = new();

    public double RollDamage()
    {
        bool isCrit = Rng.NextDouble() < CritChance;
        double finalDamage = Damage;

        if (isCrit)
            finalDamage *= CritMultiplier;

        return finalDamage;
    }

    public abstract double WeaponDamage();
}