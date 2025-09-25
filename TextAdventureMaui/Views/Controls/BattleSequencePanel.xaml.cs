namespace TextAdventureMaui.Views.Controls;

public partial class BattleSequencePanel : ContentView
{
    public static readonly BindableProperty EnemySequenceProperty =
        BindableProperty.Create(nameof(EnemySequence), typeof(IEnumerable<string>), typeof(BattleSequencePanel));

    public IEnumerable<string> EnemySequence
    {
        get => (IEnumerable<string>)GetValue(EnemySequenceProperty);
        set => SetValue(EnemySequenceProperty, value);
    }

    public static readonly BindableProperty CurrentInputProperty =
        BindableProperty.Create(nameof(CurrentInput), typeof(IEnumerable<string>), typeof(BattleSequencePanel));

    public IEnumerable<string> CurrentInput
    {
        get => (IEnumerable<string>)GetValue(CurrentInputProperty);
        set => SetValue(CurrentInputProperty, value);
    }

    public static readonly BindableProperty IsEnemyTurnProperty =
        BindableProperty.Create(nameof(IsEnemyTurn), typeof(bool), typeof(BattleSequencePanel));

    public bool IsEnemyTurn
    {
        get => (bool)GetValue(IsEnemyTurnProperty);
        set => SetValue(IsEnemyTurnProperty, value);
    }

    public BattleSequencePanel()
    {
        InitializeComponent();
    }
}