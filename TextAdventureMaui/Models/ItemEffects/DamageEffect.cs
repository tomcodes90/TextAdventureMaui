

namespace TextAdventureMaui.Models.ItemEffects
{
    public class HpEffect : Effect
    {
        public HpEffect(int amount) : base(EffectType.Heal, amount) { }

        public override void Apply(Player player)
        {
            player.HP = Math.Min(player.MaxHP, player.HP + Amount);
        }
    }
}
