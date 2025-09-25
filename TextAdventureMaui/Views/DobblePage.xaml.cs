using TextAdventureMaui.ViewModels;

namespace TextAdventureMaui.Views;

public partial class DobblePage : ContentPage
{
    public DobblePage(DobbleViewModel vm)   // DI will pass this in
    {
        InitializeComponent();              // must exist
        BindingContext = vm;
    }
}
