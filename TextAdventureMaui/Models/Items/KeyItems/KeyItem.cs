using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Items.KeyItems
{
    using TextAdventureMaui.Models.Items;

    namespace TextAdventureMaui.Models.Items.KeyItems
    {
        public class KeyItem(string name, string description, string unlocks) : Item(name, description)
        {
            // Reference to what this key can unlock
            public string Unlocks { get; } = unlocks;

            // Optional: helper method to check if this key unlocks a target
            public bool CanUnlock(string target) => Unlocks == target;

            public override string ToString() => $"{Name} (Unlocks: {Unlocks})";
        }
    }

}
