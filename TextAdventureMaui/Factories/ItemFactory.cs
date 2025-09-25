using System.Text.Json;
using TextAdventureMaui.Models.Items;
using TextAdventureMaui.Models.ItemEffects;
using TextAdventureMaui.Models.Items.KeyItems;

namespace TextAdventureMaui.Factories;

public class ItemFactory
{
    private readonly Dictionary<string, Item> _items = new();

    public ItemFactory()
    {
        LoadItemsAsync().Wait();
    }

    private async Task LoadItemsAsync()
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("items.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            var items = JsonSerializer.Deserialize<List<ItemDefinition>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (items != null)
            {
                foreach (var def in items)
                {
                    _items[def.Name] = def.ToItem();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Failed to load items.json: {ex.Message}");
        }
    }

    public Item CreateItem(string name)
    {
        if (_items.TryGetValue(name, out var item))
            return item;

        throw new KeyNotFoundException($"Item '{name}' not found in items.json.");
    }
}

internal class ItemDefinition
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Type { get; set; } = "Generic"; // "Trinket", "KeyItem"
    public bool IsStackable { get; set; } = true;

    // Specific fields
    public string? Unlocks { get; set; } // For KeyItem
    public string? EffectType { get; set; } // "BuffHp" or "BuffDamage"
    public int EffectAmount { get; set; }
    public string? EffectDescription { get; set; }

    public Item ToItem()
    {
        return Type switch
        {
            "KeyItem" => new Models.Items.KeyItems.KeyItem(Name, Description, Unlocks ?? ""),
            "Trinket" => new Trinket(Name, Description, CreateEffect()),
            _ => new GenericItem(Name, Description, IsStackable)
        };
    }

    private ItemEffect CreateEffect()
    {
        return EffectType switch
        {
            "BuffHp" => new HpItemEffect(EffectDescription ?? "Increases HP", EffectAmount),
            "BuffDamage" => new DamageItemEffect(EffectDescription ?? "Increases Damage", EffectAmount),
            _ => throw new Exception($"Unknown effect type: {EffectType}")
        };
    }
}

internal class GenericItem : Item
{
    public GenericItem(string name, string description, bool isStackable = true)
        : base(name, description, isStackable) { }
}
