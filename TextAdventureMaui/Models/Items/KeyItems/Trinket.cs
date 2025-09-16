using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Items.KeyItems
{
    public class Trinket : Item
    {
        public Microsoft.Maui.Controls.Effect Effect { get; }

        public Trinket(string name, string description, Microsoft.Maui.Controls.Effect effect)
            : base(name, description)
        {
            Effect = effect;
        }

        public void Activate(Player player)
        {
            Effect.Apply(player);
        }

        public override string ToString() => $"{Name} ({Effect.Description})";
    }

}
