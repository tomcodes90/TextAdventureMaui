#if WINDOWS
using Microsoft.Maui.Platform;
using Microsoft.UI.Windowing;
using WinRT;
#endif
using Microsoft.Maui.Controls;

namespace TextAdventureMaui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var shell = new AppShell();
        var window = new Window(shell);

#if WINDOWS
        window.HandlerChanged += (s, e) =>
        {
            // Get the WinUI3 Window from the MAUI Window
            var mauiWindow = window.Handler.PlatformView as Microsoft.UI.Xaml.Window;
            if (mauiWindow == null)
                return;

            // Get the HWND handle
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(mauiWindow);

            // Get the WindowId
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);

            // Get the AppWindow
            var appWindow = AppWindow.GetFromWindowId(windowId);

            // Set full screen
            appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
        };
#endif

        return window;
    }
}
