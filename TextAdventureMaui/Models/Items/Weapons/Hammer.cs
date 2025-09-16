using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Items.Weapons
{
    public class Hammer(string name, string description)
        : Weapon(name, description, 2)
    {
        public override double PerformAttack(Entity target, Random rng)
        {
            double totalDamage = Damage;
            target.TakeDamage(totalDamage);

            if (rng.NextDouble() < 0.2) // 20% chance
                Console.WriteLine($"{Name} stuns {target.Name}!");

            return totalDamage;
        }
    }

}
