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
        public override double PerformAttack(Entity target, Random rng)
        {
            double totalDamage = Damage;
            if (rng.NextDouble() < 0.25) // 25% crit chance
            {
                Console.WriteLine($"{Name} lands a critical hit!");
                totalDamage *= 2;
            }

            target.TakeDamage(totalDamage);
            return totalDamage;
        }
    }

}
