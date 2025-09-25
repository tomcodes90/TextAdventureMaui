namespace TextAdventureMaui.Models.Items.Weapons
{
    public class Sword : Weapon
    {
        public Sword() 
            : base("Sword", "Una spada bilanciata", 3, 0.2)
        {
            Icon = "sword.png";       // 🔹 your actual file
            DisplayName = "Sword";
        }

        public override double WeaponDamage() => RollDamage();
    }
}