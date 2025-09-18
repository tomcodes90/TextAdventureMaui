using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Models.Missions;

namespace TextAdventureMaui.ViewModels
{
    public class BattleViewModel : INotifyPropertyChanged
    {
        // Backing fields
        private string battleLog = "Battle starts!";
        private int playerHp;
        private int enemyHp;

        private Queue<DodgeInput> expectedCombo = new();
        private EnemyAttack? currentAttack;

        public double PlayerHpPercent => (double)PlayerHp / Player.MaxHp;
        public double EnemyHpPercent => (double)EnemyHp / Enemy.MaxHp;


        // Constructor
        public BattleViewModel(Player player, Enemy enemy)
        {
            Player = player;
            Enemy = enemy;

            PlayerHp = player.MaxHp;
            EnemyHp = enemy.MaxHp;

            PlayerAttackCommand = new Command(PlayerAttack);
            PressInputCommand = new Command<DodgeInput>(PressInput);
        }

        // Public entities
        public Player Player { get; }
        public Enemy Enemy { get; }

        // Bindable properties
        public string BattleLog
        {
            get => battleLog;
            set { battleLog = value; OnPropertyChanged(); }
        }

        public int PlayerHp
        {
            get => playerHp;
            set
            {
                playerHp = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PlayerHpPercent));
            }
        }

        public int EnemyHp
        {
            get => enemyHp;
            set
            {
                enemyHp = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EnemyHpPercent));
            }
        }


        // Combo handling
        public ObservableCollection<DodgeInput> CurrentCombo { get; } = new();

        // Commands
        public ICommand PlayerAttackCommand { get; }
        public ICommand PressInputCommand { get; }

        // Logic
        private void PlayerAttack()
        {
            int damage = (int)Math.Ceiling(Player.DealDamage()); // later: weapon multiplier
            EnemyHp -= damage;
            if (EnemyHp < 0) EnemyHp = 0;
            BattleLog = $"{Player.Name} hits {Enemy.Name} for {damage}!";
        }

        public void EnemyTurn(EnemyAttack attack)
        {
            currentAttack = attack;
            expectedCombo = new Queue<DodgeInput>(attack.Combo);
            CurrentCombo.Clear();
            BattleLog = $"{Enemy.Name} uses {attack.Name}! Dodge!";
        }

        private void PressInput(DodgeInput input)
        {
            if (expectedCombo.Count == 0) return;

            var expected = expectedCombo.Dequeue();
            CurrentCombo.Add(input);

            if (input != expected)
            {
                PlayerHp -= currentAttack?.Damage ?? 0;
                if (PlayerHp < 0) PlayerHp = 0;

                BattleLog = $"You failed to dodge! Took {currentAttack?.Damage} damage!";
                expectedCombo.Clear();
            }
            else if (expectedCombo.Count == 0)
            {
                BattleLog = "You dodged successfully!";
            }
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
