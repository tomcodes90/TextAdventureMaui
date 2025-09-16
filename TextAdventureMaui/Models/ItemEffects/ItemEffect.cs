using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.ItemEffects
{
    public abstract class ItemEffect(EffectType type, string description, int amount)
    {
        public EffectType Type { get; protected set; } = type;
        public string Description { get; protected set; } = description;
        public int Amount { get; protected set; } = amount;

        public abstract void Apply(Player player);
    }
}
