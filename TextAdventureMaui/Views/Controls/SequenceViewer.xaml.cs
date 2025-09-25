namespace TextAdventureMaui.Views.Controls;

public partial class SequenceViewer : ContentView
{
    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable<string>),
            typeof(SequenceViewer),
            default(IEnumerable<string>),
            propertyChanged: OnItemsSourceChanged);

    public IEnumerable<string> ItemsSource
    {
        get => (IEnumerable<string>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public SequenceViewer()
    {
        InitializeComponent();
    }

    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SequenceViewer viewer && newValue is IEnumerable<string> items)
        {
            viewer.Collection.ItemsSource = items;
        }
        else if (bindable is SequenceViewer v)
        {
            v.Collection.ItemsSource = null;
        }
    }
}