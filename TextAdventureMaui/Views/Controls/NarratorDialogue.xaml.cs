using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace TextAdventureMaui.Views.Controls;

public partial class NarratorDialogue : ContentView
{
    public NarratorDialogue()
    {
        InitializeComponent();
    }

    // Bindable text property (final text to display)
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(NarratorDialogue), string.Empty, propertyChanged: OnTextChanged);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    // Optional icon
    public static readonly BindableProperty IconProperty =
        BindableProperty.Create(nameof(Icon), typeof(string), typeof(NarratorDialogue), string.Empty);

    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    // Typewriter speed (ms per character)
    public static readonly BindableProperty TypeSpeedProperty =
        BindableProperty.Create(nameof(TypeSpeed), typeof(int), typeof(NarratorDialogue), 30);

    public int TypeSpeed
    {
        get => (int)GetValue(TypeSpeedProperty);
        set => SetValue(TypeSpeedProperty, value);
    }

    private static async void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NarratorDialogue dialogue && newValue is string newText)
        {
            await dialogue.ShowTextTypewriter(newText);
        }
    }

    private async Task ShowTextTypewriter(string fullText)
    {
        Label textLabel = Root.FindByName<Label>("Label"); // if you give your Label x:Name="Label"
        textLabel.Text = string.Empty;

        foreach (char c in fullText)
        {
            textLabel.Text += c;
            await Task.Delay(TypeSpeed);
        }
    }
}