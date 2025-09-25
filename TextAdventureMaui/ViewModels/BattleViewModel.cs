using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Models.Missions;
using TextAdventureMaui.Services;

namespace TextAdventureMaui.ViewModels;

public partial class BattleViewModel : ObservableObject
{
    private readonly BattleService _battleService;

    public Player Player { get; }
    public Enemy Enemy { get; }

    [ObservableProperty] private string battleLog = "La battaglia ha inizio!";

    [ObservableProperty] private ObservableCollection<string> enemySequence = new();
    [ObservableProperty] private ObservableCollection<string> currentInput = new();

    [ObservableProperty] private bool isEnemyTurn;

    private List<string> _currentEnemySequence = new();
    private int _sequenceIndex = 0;
    private double _enemyAttackDamage;

    public event EventHandler<ChallengeResult>? BattleEnded;

    // 🔹 proprietà per binding in XAML
    public double EnemyHpPercent => Enemy.CurrentHp / (double)Enemy.MaxHp;
    public double PlayerHpPercent => Player.CurrentHp / (double)Player.MaxHp;

    public BattleViewModel(Player player, Enemy enemy)
    {
        Player = player;
        Enemy = enemy;
        _battleService = new BattleService(player, enemy);

        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        if (_battleService.IsBattleOver())
        {
            EndBattle();
            return;
        }

        IsEnemyTurn = false;
        BattleLog = "È il tuo turno! Scegli un'abilità.";

        CurrentInput = new ObservableCollection<string>();
    }

    private void StartEnemyTurn()
    {
        if (_battleService.IsBattleOver())
        {
            EndBattle();
            return;
        }

        IsEnemyTurn = true;

        var (message, sequence, damage) = _battleService.EnemyTurn();
        BattleLog = message;

        _currentEnemySequence = sequence;
        _sequenceIndex = 0;
        _enemyAttackDamage = damage;

        EnemySequence = new ObservableCollection<string>(sequence);
    }

    [RelayCommand]
    private void InputDirection(string direction)
    {
        if (IsEnemyTurn)
        {
            if (_sequenceIndex < _currentEnemySequence.Count &&
                _currentEnemySequence[_sequenceIndex] == direction)
            {
                _sequenceIndex++;
                BattleLog = $"Sequenza corretta ({_sequenceIndex}/{_currentEnemySequence.Count})";

                if (_sequenceIndex >= _currentEnemySequence.Count)
                {
                    BattleLog = "Hai schivato l'attacco!";
                    StartPlayerTurn();
                }
            }
            else
            {
                Player.TakeDamage(_enemyAttackDamage);
                BattleLog = $"{Enemy.Name} ti colpisce! Perdi {_enemyAttackDamage} HP.";

                // 🔹 notifica cambiamento HP
                OnPropertyChanged(nameof(PlayerHpPercent));
                StartPlayerTurn();
            }
        }
        else
        {
            CurrentInput.Add(direction);
            BattleLog = $"Combo: {string.Join(" ", CurrentInput)}";
        }
    }

    [RelayCommand]
    private void ConfirmInput()
    {
        if (IsEnemyTurn) return;

        var inputList = CurrentInput.ToList();
        var result = _battleService.PlayerTurn(inputList);

        BattleLog = result;

        // 🔹 notifica cambiamento HP del nemico
        OnPropertyChanged(nameof(EnemyHpPercent));

        CurrentInput = new ObservableCollection<string>();
        StartEnemyTurn();
    }

    [RelayCommand]
    private void ClearInput()
    {
        if (IsEnemyTurn) return;

        CurrentInput = new ObservableCollection<string>();
        BattleLog = "Input cancellato. Reinserisci la combo.";
    }

    private void EndBattle()
    {
        var result = _battleService.GetResult();

        var reward = new ChallengeReward(
            result.PlayerWon,
            canChooseUpgrade: result.PlayerWon,
            newAbilityUnlockedId: result.PlayerWon ? 2 : null,
            loot: result.Loot
        );

        BattleEnded?.Invoke(this, reward);

        BattleLog = reward.PlayerWon ? "Hai vinto la battaglia!" : "Hai perso...";
    }
}
