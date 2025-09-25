using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Dispatching;
using System.Diagnostics;
using TextAdventureMaui.Services;

namespace TextAdventureMaui.ViewModels;

public partial class DobbleViewModel : ObservableObject
{
    private readonly DobbleService _dobbleService;
    private IDispatcherTimer? _roundTimer;

    // --- Game Constants ---
    private const int RoundTime = 5;   // seconds per round
    private const int MaxFails = 2;    // allowed fails
    private const int TargetScore = 10; // score to win

    // --- Bindable Properties ---
    [ObservableProperty] private List<string> card1 = new();
    [ObservableProperty] private List<string> card2 = new();
    [ObservableProperty] private string? match;
    [ObservableProperty] private int score;
    [ObservableProperty] private int fails;
    [ObservableProperty] private int timeLeft;
    [ObservableProperty] private bool gameOver;
    [ObservableProperty] private string gameMessage = string.Empty;

    public DobbleViewModel(DobbleService service)
    {
        _dobbleService = service;
        StartNewGame();
    }
    [ObservableProperty] private int timeLeftMs;

// progress bar value
    public double TimeProgress => (double)TimeLeftMs / (RoundTime * 1000);

// formatted seconds
    public double TimeLeftSeconds => timeLeftMs / 1000.0;

[ObservableProperty] private Color timerColor = Colors.Red;

    partial void OnTimeLeftMsChanged(int value)
    {
        OnPropertyChanged(nameof(TimeProgress));
        OnPropertyChanged(nameof(TimeLeftSeconds));

        // Change color in last second
        if (TimeLeftMs <= 1000 && !GameOver)
        {
            // Flash orange/red every 200ms
            if ((TimeLeftMs / 200) % 2 == 0)
                TimerColor = Colors.Orange;
            else
                TimerColor = Colors.Red;
        }
        else
        {
            TimerColor = Colors.Red; // default
        }
    }

    // --- Commands ---

    [RelayCommand]
    private void StartNewGame()
    {
        Score = 0;
        Fails = 0;
        GameOver = false;
        GameMessage = "";
        _ = LoadNewPairAsync();
    }

    [RelayCommand]
    private async Task LoadNewPairAsync()
    {
        if (GameOver) return;

        var (c1, c2) = await _dobbleService.GetCardPairAsync();
        Card1 = c1;
        Card2 = c2;
        Match = await _dobbleService.FindMatchAsync(c1, c2);

        Debug.WriteLine($"New pair. Match = {Match}");

        StartTimer();
    }

    [RelayCommand]
    private void SelectIcon(string icon)
    {
        if (GameOver) return;

        StopTimer();

        if (icon == Match)
        {
            Score++;
            if (Score >= TargetScore)
            {
                EndGame(true);
                return;
            }
            _ = LoadNewPairAsync();
        }
        else
        {
            Fails++;
            if (Fails > MaxFails)
            {
                EndGame(false);
                return;
            }
            _ = LoadNewPairAsync();
        }
    }

    // --- Timer Logic ---
    private void StartTimer()
    {
        StopTimer();

        TimeLeftMs = RoundTime * 1000; // reset to 5000ms

        _roundTimer = Application.Current.Dispatcher.CreateTimer();
        _roundTimer.Interval = TimeSpan.FromMilliseconds(100);
        _roundTimer.Tick += (s, e) =>
        {
            TimeLeftMs -= 100;

            if (TimeLeftMs <= 0)
            {
                StopTimer();
                Fails++;
                if (Fails > MaxFails)
                    EndGame(false);
                else
                    _ = LoadNewPairAsync();
            }
        };
        _roundTimer.Start();
    }


    private void StopTimer()
    {
        if (_roundTimer != null)
        {
            _roundTimer.Stop();
            _roundTimer = null;
        }
    }

    private void EndGame(bool won)
    {
        StopTimer();
        GameOver = true;
        GameMessage = won ? "🎉 You Win!" : "❌ Game Over!";
    }
}
