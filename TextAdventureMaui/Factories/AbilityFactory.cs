using System.Text.Json;
using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Entities;

namespace TextAdventureMaui.Services;

public class AbilityFactory
{
    private readonly Dictionary<int, AbilityData> _abilities;

    public AbilityFactory()
    {
        // Carichiamo abilities.json da Resources/Data
        using var stream = FileSystem.OpenAppPackageFileAsync("Data/abilities.json").Result;
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        var abilityList = JsonSerializer.Deserialize<List<AbilityData>>(json)!;
        _abilities = abilityList.ToDictionary(a => a.Id);
    }

    public Ability CreateAbilityById(int id)
    {
        if (!_abilities.TryGetValue(id, out var data))
            throw new ArgumentException($"Ability with id {id} not found.");

        return new Ability(
            data.Name,
            data.InputSequence,
            data.Damage,
            data.Description
        );
    }

    private class AbilityData
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public List<string> InputSequence { get; set; } = new();
        public int Damage { get; set; }
    }
}