namespace TextAdventureMaui.Models.Items.Weapons
{
   public abstract class Weapon(string name, string description, double damage)
        : Item(name, description, true)
    {
        public double Damage { get; protected set; } = damage;

        // Each weapon defines how it attacks
        public abstract double WeaponDamage(Random rng);
    }
}

