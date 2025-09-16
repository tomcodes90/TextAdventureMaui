using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.ItemEffects
{
    public class HpEffect : Effect
    {
        public HpEffect(int amount) : base(EffectType.Heal, amount) { }

        public override void Apply(Player player)
        {
            player. = Math.Min(player.MaxHP, player.HP + Amount);
        }
    }
}
