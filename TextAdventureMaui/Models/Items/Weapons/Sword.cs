namespace TextAdventureMaui.Models.Items.Weapons
{
    public class Sword(string name, string description)
        : Weapon(name, description, 1)
    {
        public override double WeaponDamage(Random rng)
        {
            double totalDamage = Damage;
            if (!(rng.NextDouble() < 0.3)) return totalDamage; // 30% chance
            Console.WriteLine($"{Name} strikes twice!");
            totalDamage += Damage;

            return totalDamage;
        }
    }

}
