using TextAdventureMaui.ViewModels;

namespace TextAdventureMaui.Views;

public partial class BattlePage : ContentPage
{
    public BattlePage()
    {
        InitializeComponent();
    }

    // convenience constructor that accepts the VM
    public BattlePage(BattleViewModel vm) : this()
    {
        BindingContext = vm;
    }
}