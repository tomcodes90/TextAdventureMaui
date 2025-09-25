using TextAdventureMaui.Models.Entities;

namespace TextAdventureMaui.Services;

public interface IEnemyProvider
{
    Enemy GetEnemyByName(string name);
    List<Enemy> GetAllEnemies();
}
