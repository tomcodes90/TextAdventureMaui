

using System.Text.Json;
using TextAdventureMaui.Models.Entities;

namespace TextAdventureMaui.Services;

public class AbilityLoader
{
    private List<Ability>? _abilities;

    public async Task<List<Ability>> LoadAbilitiesAsync()
    {
        if (_abilities != null)
            return _abilities;

        using var stream = await FileSystem.OpenAppPackageFileAsync("abilities.json");
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();

        var abilityDtos = JsonSerializer.Deserialize<List<AbilityDto>>(json);
        if (abilityDtos == null)
            return new List<Ability>();

        _abilities = abilityDtos.Select(dto =>
            new Ability(dto.Name, dto.InputSequence, dto.Damage, dto.Description)
        ).ToList();

        return _abilities;
    }

    // Pick abilities by weapon type
    public List<Ability> GetStarterAbilities(string weaponChoice)
    {
        return weaponChoice switch
        {
            "Sword" => _abilities!.Where(a => a.Name is "Slash" or "Whirlwind" or "Dark Slash").ToList(),
            "Bow"   => _abilities!.Where(a => a.Name is "Piercing Arrow" or "Dramatic Hair Flip").ToList(),
            "Staff" => _abilities!.Where(a => a.Name is "Grandma’s Wrath" or "Apocalypse Burrito").ToList(),
            _       => new List<Ability>()
        };
    }

    // internal DTO for JSON mapping
    private class AbilityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public List<string> InputSequence { get; set; } = new();
        public int Damage { get; set; }
    }
}
