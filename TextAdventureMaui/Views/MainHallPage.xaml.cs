using TextAdventureMaui.ViewModels;

namespace TextAdventureMaui.Views;

public partial class MainHallPage : ContentPage
{
    private bool _isAnimatingOverlay;

    public MainHallPage(MainHallViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        // Listen for exclusivity close events
        vm.RequestClosePanel += async panel =>
        {
            if (panel == "Player") await FadeOutPanel(PlayerPanelControl);
            if (panel == "Maidy")  await FadeOutPanel(MaidyDialoguePanel);
            if (panel == "Chex")   await FadeOutPanel(ChexDialoguePanel);
        };

        vm.PropertyChanged += async (s, e) =>
        {
            // Overlay fade
            if (e.PropertyName == nameof(MainHallViewModel.ShowOverlay))
            {
                if (vm.ShowOverlay && !_isAnimatingOverlay)
                {
                    _isAnimatingOverlay = true;
                    OverlayView.IsVisible = true;
                    OverlayView.Opacity = 0;
                    await OverlayView.FadeTo(1, 300, Easing.CubicInOut);
                    _isAnimatingOverlay = false;
                }
                else if (!vm.ShowOverlay && !_isAnimatingOverlay)
                {
                    _isAnimatingOverlay = true;
                    await OverlayView.FadeTo(0, 200, Easing.CubicInOut);
                    OverlayView.IsVisible = false;
                    _isAnimatingOverlay = false;
                }
            }

            // Player panel bounce in
            if (e.PropertyName == nameof(MainHallViewModel.ShowPlayerPanel) && vm.ShowPlayerPanel)
                await BounceIn(PlayerPanelControl);

            // Maid dialogue bounce in
            if (e.PropertyName == nameof(MainHallViewModel.ShowMaidyDialogue) && vm.ShowMaidyDialogue)
                await BounceIn(MaidyDialoguePanel);

            // Chex dialogue bounce in
            if (e.PropertyName == nameof(MainHallViewModel.ShowChexDialogue) && vm.ShowChexDialogue)
                await BounceIn(ChexDialoguePanel);
        };
    }

    private async Task FadeOutPanel(View panel)
    {
        if (panel.IsVisible)
        {
            await panel.FadeTo(0, 200, Easing.CubicOut);
            panel.IsVisible = false;
            panel.Opacity = 1; // reset for next time
        }
    }

    private async Task BounceIn(View panel)
    {
        panel.Opacity = 0;
        panel.Scale = 0.9;

        await panel.FadeTo(1, 200, Easing.CubicInOut);
        await panel.ScaleTo(1.05, 150, Easing.CubicOut);
        await panel.ScaleTo(1.0, 100, Easing.CubicIn);
    }
}
