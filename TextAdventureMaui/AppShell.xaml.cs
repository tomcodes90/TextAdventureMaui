using TextAdventureMaui.Views;

namespace TextAdventureMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(RoomPage), typeof(RoomPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(BattlePage), typeof(BattlePage));
        Routing.RegisterRoute(nameof(MainMenuPage), typeof(MainMenuPage));

    }
}
