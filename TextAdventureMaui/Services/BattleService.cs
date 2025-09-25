using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Models.Missions;

namespace TextAdventureMaui.Services;

public class BattleService
{
    private readonly Player _player;
    private readonly Enemy _enemy;
    private readonly Random _rng = new();

    public bool IsPlayerTurn { get; private set; } = true;

    public BattleService(Player? player, Enemy enemy)
    {
        _player = player;
        _enemy = enemy;
    }

    public string PlayerTurn(List<string> inputSequence)
    {
        if (!IsPlayerTurn) return "Non è il turno del player!";

        var ability = _player.MatchAbility(inputSequence);

        if (ability != null)
        {
            _enemy.TakeDamage(ability.Damage);
            IsPlayerTurn = false; // passa al nemico
            return $"{_player.Name} usa {ability.Name} e infligge {ability.Damage} danni!";
        }
        else
        {
            _enemy.TakeDamage(_player.BaseAttack);
            IsPlayerTurn = false; // passa al nemico
            return $"{_player.Name} sbaglia combo! Attacco base infligge {_player.BaseAttack} danni.";
        }
    }

    public (string Message, List<string> Sequence, double Damage) EnemyTurn()
    {
        if (IsPlayerTurn) return ("Non è il turno del nemico!", new List<string>(), 0);

        var directions = new[] { "Up", "Down", "Left", "Right" };
        int length = _rng.Next(2, 4);

        var sequence = Enumerable.Range(0, length)
            .Select(_ => directions[_rng.Next(directions.Length)])
            .ToList();

        double damage = _enemy.DealDamage();

        IsPlayerTurn = true; // torna al player
        return ($"{_enemy.Name} attacca! Premi la sequenza per schivare.", sequence, damage);
    }

    public bool IsBattleOver()
    {
        return _player.CurrentHp <= 0 || _enemy.CurrentHp <= 0;
    }
}
