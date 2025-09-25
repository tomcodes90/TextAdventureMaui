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

        // 🔹 Ensure we land on MainMenu at startup
   

#if WINDOWS
    window.HandlerChanged += (s, e) =>
    {
        var mauiWindow = window.Handler.PlatformView as Microsoft.UI.Xaml.Window;
        if (mauiWindow == null)
            return;

        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(mauiWindow);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWindow = AppWindow.GetFromWindowId(windowId);

        appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
    };
#endif

        return window;
    }

}
