namespace TextAdventureMaui.Models.Items.Weapons
{
    public class Hammer() : Weapon("Hammer", "Un martello pesante e devastante", damage: 4, critChance: 0.1,
        critMultiplier: 2.5)
    {
        public override double WeaponDamage()
        {
            return RollDamage();
        }
    }
}