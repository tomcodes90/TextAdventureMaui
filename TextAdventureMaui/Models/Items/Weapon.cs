using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Items
{
    public class Weapon(string name, string description, bool isEquippable) : Item(name, description, isEquippable)
    {
        public int Damage { get; set; }
    }
}
