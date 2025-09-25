using TextAdventureMaui.Models.Items;
using TextAdventureMaui.Models.ItemEffects;
using TextAdventureMaui.Models.Items.KeyItems;

namespace TextAdventureMaui.Models;

public class Inventory
{
    private readonly Dictionary<Item, int> _items = new();
    private readonly Player _owner; // 👈 wichtig: wem gehört das Inventar?

    public Inventory(Player owner)
    {
        _owner = owner;
    }
    public Inventory()
    {
        
    }

    public IReadOnlyDictionary<Item, int> Items => _items;

    public void AddItem(Item item, int quantity = 1)
    {
        if (item.IsStackable && _items.ContainsKey(item))
        {
            _items[item] += quantity;
        }
        else
        {
            _items[item] = quantity;
        }

        // 🔹 Wenn es ein Trinket ist → Effekt sofort anwenden
        if (item is Trinket trinket && trinket.Effect != null)
        {
            trinket.Effect.Apply(_owner);
            Console.WriteLine($"[Inventory] Effect applied: {trinket.Effect.Description}");
        }
    }

    public void RemoveItem(Item item, int quantity = 1)
    {
        if (!_items.ContainsKey(item))
            return;

        _items[item] -= quantity;
        if (_items[item] <= 0)
            _items.Remove(item);

        // 🔹 Falls es ein Trinket war → Effekt rückgängig machen
        if (item is Trinket trinket && trinket.Effect != null)
        {
            RemoveEffect(trinket.Effect);
            Console.WriteLine($"[Inventory] Effect removed: {trinket.Effect.Description}");
        }
    }

    private void RemoveEffect(ItemEffect effect)
    {
        // 🔹 Da deine Effekte direkt Stats erhöhen, müssen wir einen Gegen-Effekt anwenden
        switch (effect)
        {
            case HpItemEffect hp:
                _owner.MaxHp -= hp.Amount;
                if (_owner.CurrentHp > _owner.MaxHp)
                    _owner.CurrentHp = _owner.MaxHp;
                break;

            case DamageItemEffect dmg:
                _owner.BaseAttack -= dmg.Amount;
                break;
        }
    }

    public bool HasItem(string itemName) =>
        _items.Keys.Any(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
}
