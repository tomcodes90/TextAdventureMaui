using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TextAdventureMaui.ViewModels
{
    public partial class CharacterViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string sprite;

        // Bind to this in CharacterView.xaml
        public IAsyncRelayCommand TapCommand { get; }

        public CharacterViewModel(string name, string sprite, Func<Task> onTapAsync)
        {
            this.name = name;
            this.sprite = sprite;
            TapCommand = new AsyncRelayCommand(onTapAsync);
        }
    }
}