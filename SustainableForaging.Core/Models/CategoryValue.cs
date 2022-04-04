using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustainableForaging.Core.Models
{
    public class CategoryValue
    {
        public Category Category { get; set; }
        public decimal TotalKilograms { get; set; }
        public decimal TotalValue { get; set; }
    }
}
