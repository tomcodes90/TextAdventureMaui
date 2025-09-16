using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.ItemEffects
{
    public abstract class Effect(EffectType type, int amount) : IEffect
    {
        public EffectType Type { get; protected set; } = type;
        public int Amount { get; protected set; } = amount;

        public abstract void Apply(Player player);
    }
}
