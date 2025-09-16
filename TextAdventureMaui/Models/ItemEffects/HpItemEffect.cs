namespace TextAdventureMaui.Models.ItemEffects
{
    public class HpItemEffect(string description, int amount) : ItemEffect(EffectType.BuffHp, description, amount)
    {
        public override void Apply(Player player)
        {
            player.MaxHp += Amount;
        }
    }
}
