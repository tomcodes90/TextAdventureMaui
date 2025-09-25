namespace TextAdventureMaui.Models.Items.Weapons
{
    public class Hammer : Weapon
    {
        public Hammer()
            : base("Hammer", "Un martello pesante e devastante", damage: 4, critChance: 0.1, critMultiplier: 2.5)
        {
            Icon = "hammer.png";     // 🔹 your actual file
            DisplayName = "Hammer";
        }

        public override double WeaponDamage() => RollDamage();
    }
}