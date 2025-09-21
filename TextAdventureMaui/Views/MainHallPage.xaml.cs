using TextAdventureMaui.ViewModels;

namespace TextAdventureMaui.Views;

public partial class MainHallPage : ContentPage
{
    public MainHallPage(MainHallViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
