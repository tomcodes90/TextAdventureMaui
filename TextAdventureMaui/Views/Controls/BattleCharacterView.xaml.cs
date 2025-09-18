namespace TextAdventureMaui.Views.Controls;

public partial class BattleCharacterView : ContentView
{
    public BattleCharacterView()
    {
        InitializeComponent();
    }

    // Portrait image
    public static readonly BindableProperty ImageProperty =
        BindableProperty.Create(nameof(Image), typeof(string), typeof(BattleCharacterView), default(string));
    public string Image
    {
        get => (string)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    // Character name
    public static readonly BindableProperty CharacterNameProperty =
        BindableProperty.Create(nameof(CharacterName), typeof(string), typeof(BattleCharacterView), string.Empty);
    public string CharacterName
    {
        get => (string)GetValue(CharacterNameProperty);
        set => SetValue(CharacterNameProperty, value);
    }

    // HP Percent (0–1) with smooth animation
    public static readonly BindableProperty HpPercentProperty =
        BindableProperty.Create(
            nameof(HpPercent),
            typeof(double),
            typeof(BattleCharacterView),
            1.0,
            propertyChanged: async (bindable, _, newVal) =>
            {
                if (bindable is BattleCharacterView view && view.HpProgress != null)
                {
                    var percent = Math.Clamp((double)newVal, 0, 1);
                    await view.HpProgress.ProgressTo(percent, 300, Easing.CubicOut);
                }
            });

    public double HpPercent
    {
        get => (double)GetValue(HpPercentProperty);
        set => SetValue(HpPercentProperty, value);
    }

    // Border brush
    public static readonly BindableProperty BorderBrushProperty =
        BindableProperty.Create(nameof(BorderBrush), typeof(Color), typeof(BattleCharacterView), Colors.Transparent);
    public Color BorderBrush
    {
        get => (Color)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    // HP bar fill color
    public static readonly BindableProperty BarBrushProperty =
        BindableProperty.Create(nameof(BarBrush), typeof(Color), typeof(BattleCharacterView), Colors.Green);
    public Color BarBrush
    {
        get => (Color)GetValue(BarBrushProperty);
        set => SetValue(BarBrushProperty, value);
    }
}
