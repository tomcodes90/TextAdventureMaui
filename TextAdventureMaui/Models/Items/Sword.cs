

namespace TextAdventureMaui.Models.Items
{
    internal class Sword(string name, string description, bool isEquippable) : Weapon(name, description, isEquippable)
    {
       public Sword() : this("Sword", "A sharp blade, useful for combat.", true)
        {
            Damage = 15;
        }
    }
}
