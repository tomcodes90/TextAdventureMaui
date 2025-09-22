using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    [ObservableProperty] private double playerHpPercent;
    [ObservableProperty] private double enemyHpPercent;

    public ObservableCollection<string> CurrentInput { get; } = new();

    private bool _isDefending = false;
    private double _pendingDamage;

    public BattleViewModel(Player player, Enemy enemy)
    {
        Player = player;
        Enemy = enemy;
        _battleService = new BattleService(player, enemy);

        UpdateHpBars();
    }

    [RelayCommand]
    private void AddInput(string input) => CurrentInput.Add(input);

    [RelayCommand]
    private void ConfirmInput()
    {
        if (CurrentInput.Count == 0) return;

        string result;

        if (_isDefending)
        {
            result = _battleService.ResolveDefense(CurrentInput.ToList(), _pendingDamage);
            _isDefending = false;
        }
        else
        {
            result = _battleService.PlayerTurn(CurrentInput.ToList());
        }

        BattleLog = result;
        CurrentInput.Clear();
        UpdateHpBars();

        if (_battleService.IsBattleOver())
        {
            var reward = _battleService.GetResult();
            BattleLog += reward.PlayerWon ? "\nHai vinto!" : "\nHai perso...";
            return;
        }

        if (!_isDefending)
        {
            var (msg, sequence, damage) = _battleService.EnemyTurn();
            BattleLog = msg + "\nSequenza: " + string.Join(" ", sequence);
            _isDefending = true;
            _pendingDamage = damage;
        }
    }
    public event EventHandler<ChallengeReward>? BattleEnded;

    private void EndBattle()
    {
        var reward = _battleService.GetResult();
        BattleEnded?.Invoke(this, reward);
    }

    [RelayCommand]
    private void ClearInput() => CurrentInput.Clear();

    private void UpdateHpBars()
    {
        PlayerHpPercent = (double)Player.CurrentHp / Player.MaxHp;
        EnemyHpPercent = (double)Enemy.CurrentHp / Enemy.MaxHp;
    }
}
