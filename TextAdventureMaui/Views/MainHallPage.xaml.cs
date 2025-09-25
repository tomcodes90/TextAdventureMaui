using TextAdventureMaui.ViewModels;

namespace TextAdventureMaui.Views;

public partial class MainHallPage : ContentPage
{
    private bool _animOverlay;

    public MainHallPage(MainHallViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        vm.PropertyChanged += async (s, e) =>
        {
            if (e.PropertyName == nameof(MainHallViewModel.ShowOverlay))
            {
                if (vm.ShowOverlay && !_animOverlay)
                {
                    _animOverlay = true;
                    OverlayView.IsVisible = true;
                    OverlayView.Opacity = 0;
                    await OverlayView.FadeTo(1, 250, Easing.CubicInOut);
                    _animOverlay = false;
                }
                else if (!vm.ShowOverlay && !_animOverlay)
                {
                    _animOverlay = true;
                    await OverlayView.FadeTo(0, 200, Easing.CubicInOut);
                    OverlayView.IsVisible = false;
                    _animOverlay = false;
                }
            }

            if (e.PropertyName == nameof(MainHallViewModel.ShowPlayerPanel))
            {
                if (vm.ShowPlayerPanel)
                {
                    PlayerPanelControl.Opacity = 0;
                    PlayerPanelControl.Scale = 0.92;
                    await PlayerPanelControl.FadeTo(1, 200, Easing.CubicInOut);
                    await PlayerPanelControl.ScaleTo(1.02, 120, Easing.CubicOut);
                    await PlayerPanelControl.ScaleTo(1.0, 90, Easing.CubicIn);
                }
                else
                {
                    await PlayerPanelControl.FadeTo(0, 160, Easing.CubicOut);
                    PlayerPanelControl.IsVisible = false; // bound property will set true again when needed
                    PlayerPanelControl.Opacity = 1;
                    PlayerPanelControl.Scale = 1;
                }
            }
        };
    }
}
