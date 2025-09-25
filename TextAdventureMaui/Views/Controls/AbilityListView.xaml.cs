namespace TextAdventureMaui.Views.Controls;

public partial class AbilityListView : ContentView
{
    public static readonly BindableProperty AbilitiesProperty =
        BindableProperty.Create(
            nameof(Abilities),
            typeof(IEnumerable<TextAdventureMaui.Models.Entities.Ability>),
            typeof(AbilityListView),
            default(IEnumerable<TextAdventureMaui.Models.Entities.Ability>));

    public IEnumerable<TextAdventureMaui.Models.Entities.Ability> Abilities
    {
        get => (IEnumerable<TextAdventureMaui.Models.Entities.Ability>)GetValue(AbilitiesProperty);
        set => SetValue(AbilitiesProperty, value);
    }

    public AbilityListView()
    {
        InitializeComponent();
    }
}