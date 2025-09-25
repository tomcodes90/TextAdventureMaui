using System.Windows.Input;
using Microsoft.Maui.Layouts;

namespace TextAdventureMaui.Views.Controls;

public partial class DobbleCardView : ContentView
{
    public static readonly BindableProperty IconsProperty =
        BindableProperty.Create(nameof(Icons), typeof(IList<string>), typeof(DobbleCardView),
            propertyChanged: OnIconsChanged);

    public static readonly BindableProperty IconTappedCommandProperty =
        BindableProperty.Create(nameof(IconTappedCommand), typeof(ICommand), typeof(DobbleCardView));

    public IList<string> Icons
    {
        get => (IList<string>)GetValue(IconsProperty);
        set => SetValue(IconsProperty, value);
    }

    public ICommand? IconTappedCommand
    {
        get => (ICommand?)GetValue(IconTappedCommandProperty);
        set => SetValue(IconTappedCommandProperty, value);
    }

    public DobbleCardView()
    {
        InitializeComponent();
        SizeChanged += (_, __) => RenderIcons(Icons); // re-layout on size changes
    }

    private static void OnIconsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var card = (DobbleCardView)bindable;
        card.RenderIcons(newValue as IList<string>);
    }

    private void RenderIcons(IList<string>? icons)
    {
        IconLayer.Children.Clear();
        if (icons == null || icons.Count == 0) return;

        double w = CardBorder.Width > 0 ? CardBorder.Width : CardBorder.WidthRequest;
        double h = CardBorder.Height > 0 ? CardBorder.Height : CardBorder.HeightRequest;

        double cx = w / 2.0;
        double cy = h / 2.0;
        double radius = Math.Min(w, h) * 0.28;
        double iconSize = Math.Min(w, h) * 0.23;
        double angleStep = 360.0 / icons.Count;

        for (int i = 0; i < icons.Count; i++)
        {
            var icon = icons[i];
            double angleRad = (i * angleStep) * Math.PI / 180.0;

            double x = cx + radius * Math.Cos(angleRad) - iconSize / 2.0;
            double y = cy + radius * Math.Sin(angleRad) - iconSize / 2.0;

            var img = new Image
            {
                Source = icon,
                WidthRequest = iconSize,
                HeightRequest = iconSize,
                Aspect = Aspect.AspectFit
            };

            var tap = new TapGestureRecognizer
            {
                Command = IconTappedCommand,
                CommandParameter = icon
            };
            img.GestureRecognizers.Add(tap);

            AbsoluteLayout.SetLayoutBounds(img, new Rect(x, y, iconSize, iconSize));
            AbsoluteLayout.SetLayoutFlags(img, AbsoluteLayoutFlags.None);
            IconLayer.Children.Add(img);
        }
    }

}
