namespace TextAdventureMaui.Models.Items
{
    // Encapsulates inventory logic
    public class Inventory
    {
        private readonly Dictionary<Item, int> items = [];

        public void AddItem(Item item, int amount = 1)
        {
            if (item.IsStackable)
            {
                if (items.ContainsKey(item))
                    items[item] += amount;
                else
                    items[item] = amount;
            }
            else
            {
                // Each unique non-stackable item gets its own entry
                for (int i = 0; i < amount; i++)
                    items[item] = 1; // Key is already unique since it's a distinct object
            }
        }

        public bool RemoveItem(Item item, int amount = 1)
        {
            if (items.TryGetValue(item, out var currentAmount))
            {
                if (item.IsStackable)
                {
                    if (currentAmount >= amount)
                    {
                        items[item] -= amount;
                        if (items[item] == 0)
                            items.Remove(item);
                        return true;
                    }
                }
                else
                {
                    // Just remove the specific instance
                    return items.Remove(item);
                }
            }
            return false;
        }

        public int GetQuantity(Item item) =>
            items.TryGetValue(item, out var qty) ? qty : 0;

        public IReadOnlyDictionary<Item, int> GetAllItems() => items;

        public void PrintInventory()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
                return;
            }

            Console.WriteLine("Inventory:");
            foreach (var kvp in items)
                Console.WriteLine($"- {kvp.Key.Name} x{kvp.Value}");
        }
    }
}
