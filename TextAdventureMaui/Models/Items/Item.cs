namespace TextAdventureMaui.Models.Items
{
    public abstract class Item(string name, string description, bool isStackable = true)
    {
        public string Name { get; } = name;
        public string Description { get; } = description;
        public bool IsStackable { get; } = isStackable;

        // Helpful for dictionary keys
        public override bool Equals(object? obj) =>
            obj is Item item && Name == item.Name;

        public override int GetHashCode() => Name.GetHashCode();
    }
}
