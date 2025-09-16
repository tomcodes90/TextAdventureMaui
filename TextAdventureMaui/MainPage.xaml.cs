
namespace TextAdventureMaui;

public partial class MainPage
{
    private int _count;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnStartClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Start Clicked");
    }
    private void OnCounterClicked(object? sender, EventArgs e)
    {
        _count++;
        CounterBtn.Text = _count == 1 ? $"Clicked {_count} time" : $"Clicked {_count} times";
        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}