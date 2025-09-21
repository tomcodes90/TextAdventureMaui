namespace TextAdventureMaui.Models.Entities;

public class Ability
{
    public string Name { get; }
    public List<string> InputSequence { get; }  // es. ["Up", "Down", "Action"]
    public int Damage { get; }
    public string Description { get; }

    public Ability(string name, List<string> sequence, int damage, string description)
    {
        Name = name;
        InputSequence = sequence;
        Damage = damage;
        Description = description;
    }
}