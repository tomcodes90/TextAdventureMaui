using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Models.Missions;

namespace TextAdventureMaui.Services;

public class BattleService
{
    private readonly Player _player;
    private readonly Enemy _enemy;
    private readonly Random _rng = new();

    private List<string> _currentEnemySequence = new();

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

        _enemy.TakeDamage(_player.BaseAttack);
        return $"{_player.Name} sbaglia combo! Attacco base infligge {_player.BaseAttack} danni.";
    }

    public (string Message, List<string> Sequence, double Damage) EnemyTurn()
    {
        var directions = new[] { "Up", "Down", "Left", "Right" };
        int length = _rng.Next(2, 4);

        _currentEnemySequence = Enumerable.Range(0, length)
            .Select(_ => directions[_rng.Next(directions.Length)])
            .ToList();

        double damage = _enemy.DealDamage();

        return ($"{_enemy.Name} attacca! Riproduci la sequenza per schivare.", _currentEnemySequence, damage);
    }

    public string ResolveDefense(List<string> playerInput, double damage)
    {
        if (playerInput.SequenceEqual(_currentEnemySequence))
        {
            return $"{_player.Name} schiva con successo!";
        }

        _player.TakeDamage(damage);
        return $"{_player.Name} fallisce la difesa e subisce {damage} danni!";
    }

    public bool IsBattleOver() => _player.CurrentHp <= 0 || _enemy.CurrentHp <= 0;

    public ChallengeReward GetResult()
    {
        if (_player.CurrentHp <= 0)
            return new ChallengeReward(false);

        // Esempio: sblocca abilità se vinci contro un certo nemico
        string? unlocked = _enemy.Name == "Goblin" ? "Whirlwind" : null;

        return new ChallengeReward(true, unlocked, canChooseUpgrade: true);
    }
}
