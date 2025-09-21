using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Models.Missions;

using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Entities;

namespace TextAdventureMaui.Services;

public class BattleService
{
    private readonly Player _player;
    private readonly Enemy _enemy;
    private readonly Random _rng = new();

    public BattleService(Player player, Enemy enemy)
    {
        _player = player;
        _enemy = enemy;
    }

    public string PlayerTurn(List<string> inputSequence)
    {
        var ability = _player.MatchAbility(inputSequence);

        if (ability != null)
        {
            _enemy.TakeDamage(ability.Damage);
            return $"{_player.Name} usa {ability.Name} e infligge {ability.Damage} danni!";
        }
        else
        {
            _enemy.TakeDamage(_player.BaseAttack);
            return $"{_player.Name} sbaglia combo! Attacco base infligge {_player.BaseAttack} danni.";
        }
    }

    public (string Message, List<string> Sequence, int Damage) EnemyTurn()
    {
        var directions = new[] { "Up", "Down", "Left", "Right" };
        int length = _rng.Next(2, 4);

        var sequence = Enumerable.Range(0, length)
            .Select(_ => directions[_rng.Next(directions.Length)])
            .ToList();

        int damage = _enemy.Attack;

        return ($"{_enemy.Name} attacca! Premi la sequenza per schivare.", sequence, damage);
    }

    public bool IsBattleOver()
    {
        return _player.CurrentHp <= 0 || _enemy.CurrentHp <= 0;
    }

    public ChallengeReward GetResult()
    {
        if (_player.CurrentHp <= 0)
            return new ChallengeReward(false);

        // esempio: se vinci contro questo nemico sblocchi una nuova abilità
        string? unlocked = null;
        if (_enemy.Name == "Goblin")
            unlocked = "Whirlwind";

        return new ChallengeReward(true, unlocked, canChooseUpgrade: true);
    }
}
