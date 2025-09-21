using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Items.Weapons
{
    public class Bow(string name, string description)
        : Weapon(name, description, 1.5)
    {
        public override double WeaponDamage(Random rng)
        {
            double totalDamage = Damage;
            if (!(rng.NextDouble() < 0.3)) return totalDamage; // 30% chance
            Console.WriteLine($"{Name} strikes critically!");
            totalDamage += Damage * 2;

            return totalDamage;
        }
    }

}
