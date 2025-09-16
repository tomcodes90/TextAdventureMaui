

namespace TextAdventureMaui.Models.Items
{
    // Encapsulates inventory logic
    public class Inventory
    {
        private Dictionary<Item, int> items = new Dictionary<Item, int>();

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
               
                for (int i = 0; i < amount; i++)
                    items[new Item(item.Name, item.Description, false)] = 1;
            }
        }

        public bool RemoveItem(Item item, int amount = 1)
        {
            if (items.ContainsKey(item))
            {
                if (items[item] >= amount)
                {
                    items[item] -= amount;
                    if (items[item] == 0)
                        items.Remove(item);
                    return true;
                }
            }
            return false;
        }

        public int GetQuantity(Item item) =>
            items.ContainsKey(item) ? items[item] : 0;

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
