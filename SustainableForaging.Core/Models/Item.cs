using System;

namespace SustainableForaging.Core.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal DollarsPerKilogram { get; set; }

        public Item() { }

        public Item(int id, string name, Category category, decimal dollarsPerKilogram)
        {
            Id = id;
            Name = name;
            Category = category;
            DollarsPerKilogram = dollarsPerKilogram;
        }

        public override bool Equals(object obj)
        {
            return obj is Item item &&
                   Id == item.Id &&
                   Name == item.Name &&
                   Category == item.Category &&
                   DollarsPerKilogram == item.DollarsPerKilogram;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Category, DollarsPerKilogram);
        }
    }
}
