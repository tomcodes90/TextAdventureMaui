using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Items
{
    public class Item
    {
        public string Name { get; }
        public string Description { get; }
        public bool IsStackable { get; }

        public Item(string name, string description, bool isStackable = true)
        {
            Name = name;
            Description = description;
            IsStackable = isStackable;
        }

        // Helpful for dictionary keys
        public override bool Equals(object? obj) => obj is Item item && Name == item.Name;
        public override int GetHashCode() => Name.GetHashCode();
    }

}