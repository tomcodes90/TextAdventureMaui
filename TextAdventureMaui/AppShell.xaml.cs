using TextAdventureMaui.Views;

namespace TextAdventureMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes that DI knows
        Routing.RegisterRoute("mainmenu", typeof(MainMenuPage));
        Routing.RegisterRoute("mainhall", typeof(MainHallPage));
        Routing.RegisterRoute("battle", typeof(BattlePage));

        // Secondary pages
        Routing.RegisterRoute(nameof(RoomPage), typeof(RoomPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}


