

using Microsoft.Maui.Controls;
using System.Windows.Input;
namespace TextAdventureMaui.Views.Controls
{
    public partial class MenuButton : ContentView
    {
        public MenuButton()
        {
            InitializeComponent();

            // Mouse/Pointer events (works on desktop/web)
            var hoverGesture = new PointerGestureRecognizer();
            hoverGesture.PointerEntered += (s, e) => OnHover(true);
            hoverGesture.PointerExited += (s, e) => OnHover(false);
            GestureRecognizers.Add(hoverGesture);

            // Tap
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => Command?.Execute(null);
            GestureRecognizers.Add(tapGesture);
        }

        private void OnHover(bool isHovered)
        {
            MainBorder.BackgroundColor = isHovered ? Colors.LightGray : Colors.Transparent;
            // Optionally add a shadow or scale effect
            MainBorder.Scale = isHovered ? 1.05 : 1;
        }

        // Bindable properties for Text, Icon, Command
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(MenuButton), string.Empty,
                propertyChanged: (b, o, n) => ((MenuButton)b).TextLabel.Text = (string)n);

        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(nameof(Icon), typeof(string), typeof(MenuButton), string.Empty,
                propertyChanged: (b, o, n) => ((MenuButton)b).IconImage.Source = (string)n);

        public string Icon { get => (string)GetValue(IconProperty); set => SetValue(IconProperty, value); }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MenuButton), null);

        public ICommand Command { get => (ICommand)GetValue(CommandProperty); set => SetValue(CommandProperty, value); }
    }
}
