namespace TextAdventureMaui.Models.ItemEffects
{
    public class DamageItemEffect(string description, int amount) : ItemEffect(EffectType.BuffDamage, description, amount)
    {
        public override void Apply(Player player)
        {
            player.BaseAttack += Amount;
        }
    }
}
