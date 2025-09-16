        
    namespace TextAdventureMaui.Models.Items.KeyItems
    {
        public class KeyItem(string name, string description, string unlocks) : Item(name, description)
        {
            // Reference to what this key can unlock
            public string Unlocks { get; } = unlocks;
            
            public bool CanUnlock(string target) => Unlocks == target;

            public override string ToString() => $"{Name} (Unlocks: {Unlocks})";
        }
    }
