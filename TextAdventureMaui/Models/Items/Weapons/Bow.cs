namespace TextAdventureMaui.Models.Items.Weapons
{
    public class Bow : Weapon
    {
        public Bow()
            : base("Bow", "Un arco leggero ma letale a distanza", damage: 2, critChance: 0.25)
        {
        }

        public override double WeaponDamage()
        {
            return RollDamage();
        }
    }
}