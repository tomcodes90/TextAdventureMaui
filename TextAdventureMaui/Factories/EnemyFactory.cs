using System.Text.Json;
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Models.Items.Weapons;

namespace TextAdventureMaui.Factories;

public class EnemyFactory
{
    private readonly Dictionary<int, EnemyData> _enemyData;

    public EnemyFactory()
    {
        using var stream = FileSystem.OpenAppPackageFileAsync("Data/enemies.json").Result;
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        var enemies = JsonSerializer.Deserialize<List<EnemyData>>(json)!;
        _enemyData = enemies.ToDictionary(e => e.Id);
    }

    public Enemy CreateEnemy(int id)
    {
        if (!_enemyData.TryGetValue(id, out var data))
            throw new ArgumentException($"Enemy with id {id} not found.");

        var enemy = new Enemy(data.Name, data.MaxHp, data.Attack);

        // assegna un’arma in base al nome (puoi personalizzare)
        enemy.EquippedWeapon = data.Weapon switch
        {
            "Sword" => new Sword(),
            "Hammer" => new Hammer(), // esempio
            "Bow" => new Sword(),
            _              => new Sword()  // fallback
        };

        return enemy;
    }

    private class EnemyData
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int MaxHp { get; set; }
        public int Attack { get; set; }
        public string Weapon { get; set; } = "";
    }
}