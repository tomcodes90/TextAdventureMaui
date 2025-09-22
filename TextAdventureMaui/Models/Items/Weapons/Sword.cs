namespace TextAdventureMaui.Models.Items.Weapons
{
    public class Sword : Weapon
    {
        public Sword() 
            : base("Sword", "Una spada bilanciata", 3, 0.2)
        {
        }

        public override double WeaponDamage()
        {
            return RollDamage();
        }
    }
}