using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoCafe
{

    public enum MealNumber
    {
        Number1 = 1,
        Number2,
        Number3,
        Number4,
        Number5,
        Number6,
        Number7
    }

    public class CafeMenu
    {
        public CafeMenu() { }

        public CafeMenu(string name, MealNumber number, string description, string ingredients, double price)
        {
            Name = name;
            NumberOfMeal = number;
            Description = description;
            Ingredients = ingredients;
            Price = price;
        }

        public string Name { get; set; }
        public MealNumber NumberOfMeal { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public double Price { get; set; }
    }
}
