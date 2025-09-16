

using TextAdventureMaui.Models.ItemEffects;

namespace TextAdventureMaui.Models.Items.KeyItems
{
    public class Trinket(string name, string description, ItemEffect effect) : Item(name, description)
    {
        public ItemEffect Effect { get; } = effect; 

        public void Activate(Player player)
        {
            Effect.Apply(player);
        }

        public override string ToString() => $"{Name} ({Effect.Description})";
    }

}
