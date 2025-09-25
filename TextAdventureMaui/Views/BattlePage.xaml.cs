#if MACCATALYST
using UIKit;
#endif

using TextAdventureMaui.ViewModels;

namespace TextAdventureMaui.Views;

public partial class BattlePage : ContentPage
{
    public BattlePage(BattleViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        Loaded += (s, e) => KeyboardCapture.Focus();

        KeyboardCapture.HandlerChanged += (s, e) =>
        {
#if WINDOWS
            var native = KeyboardCapture.Handler.PlatformView as Microsoft.UI.Xaml.Controls.TextBox;
            if (native != null)
            {
                native.KeyDown += (sender, args) =>
                {
                    HandleSpecialKeys(args.Key.ToString().ToLower());
                };
            }
#elif MACCATALYST
            var native = KeyboardCapture.Handler.PlatformView as UITextField;
            if (native != null)
            {
                // Enter/Return
                native.ShouldReturn = textField =>
                {
                    HandleSpecialKeys("enter");
                    return false; // prevent newline
                };

                // Backspace detection
                native.ShouldChangeCharacters = (textField, range, replacement) =>
                {
                    if (string.IsNullOrEmpty(replacement) && range.Length == 1)
                    {
                        HandleSpecialKeys("backspace");
                        return false; // don’t modify text
                    }
                    return true;
                };
            }
#endif
        };
    }

    private void KeyboardCapture_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue)) return;

        var vm = BindingContext as BattleViewModel;
        char key = e.NewTextValue.Last();

        switch (char.ToLowerInvariant(key))
        {
            case 'w': vm?.InputDirectionCommand.Execute("Up"); break;
            case 'a': vm?.InputDirectionCommand.Execute("Left"); break;
            case 's': vm?.InputDirectionCommand.Execute("Down"); break;
            case 'd': vm?.InputDirectionCommand.Execute("Right"); break;
            case 'e': vm?.InputDirectionCommand.Execute("Action"); break;
        }

        KeyboardCapture.Text = string.Empty;
    }

    private void HandleSpecialKeys(string key)
    {
        var vm = BindingContext as BattleViewModel;
        switch (key)
        {
            case "enter":
                if (vm?.ConfirmInputCommand.CanExecute(null) == true)
                    vm.ConfirmInputCommand.Execute(null);
                break;

            case "backspace":
                if (vm?.ClearInputCommand.CanExecute(null) == true)
                    vm.ClearInputCommand.Execute(null);
                break;
        }
    }
}
