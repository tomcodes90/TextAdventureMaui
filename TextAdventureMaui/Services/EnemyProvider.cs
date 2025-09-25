using System.Text.Json;
using TextAdventureMaui.Models.Entities;

namespace TextAdventureMaui.Services;

public class EnemyProvider : IEnemyProvider
{
    private readonly Dictionary<string, Enemy> _enemies = new();

    public EnemyProvider()
    {
        LoadEnemiesAsync().Wait();
    }

    private async Task LoadEnemiesAsync()
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("enemies.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            var enemies = JsonSerializer.Deserialize<List<Enemy>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (enemies != null)
            {
                foreach (var e in enemies)
                {
                    // 👇 use Name as key instead of Id
                    _enemies[e.Name] = e;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Failed to load enemies.json: {ex.Message}");
        }
    }

    public Enemy GetEnemyByName(string name)
    {
        if (_enemies.TryGetValue(name, out var enemy))
            return enemy;

        throw new KeyNotFoundException($"Enemy with name '{name}' not found in enemies.json.");
    }

    public List<Enemy> GetAllEnemies() => _enemies.Values.ToList();
}