namespace TextAdventureMaui.Models.ItemEffects
{
    public interface IEffect
    {
        EffectType Type { get; }
        void Apply(Player player);
    }

}
