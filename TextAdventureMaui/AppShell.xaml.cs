using TextAdventureMaui.Views;

namespace TextAdventureMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Only for pages not in ShellContent
        Routing.RegisterRoute(nameof(RoomPage), typeof(RoomPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}

