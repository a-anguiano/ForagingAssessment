using System;

namespace SustainableForaging.Core.Models
{
    public class Forage
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public Forager Forager { get; set; }
        public Item Item { get; set; }
        public decimal Kilograms { get; set; }

        public decimal Value
        {
            get
            {
                if(Item == null)
                {
                    return decimal.Zero;
                }
                return Item.DollarsPerKilogram * Kilograms;
            }
        }
    }
}
