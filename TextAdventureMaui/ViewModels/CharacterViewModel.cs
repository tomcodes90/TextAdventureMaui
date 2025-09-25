using CommunityToolkit.Mvvm.Input;

namespace TextAdventureMaui.ViewModels;

public class CharacterViewModel
{
    public string Name { get; }
    public string Sprite { get; }
    public IRelayCommand TapCommand { get; }

    public CharacterViewModel(string name, string sprite, Func<Task> onTap)
    {
        Name = name;
        Sprite = sprite;
        TapCommand = new AsyncRelayCommand(onTap);
    }
}
