using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace PseudoMenu.MenuData
{
    internal class Dish
    {
        internal string name { get; set; }
        internal string category { get; set; } // категории блюда для меню
        internal List<Product> Recipe;
        internal void AddProduct(Product t) { Recipe.Add(t); }


        internal Dish(string t, string k1)
        {
            name = t;
            category = k1;
            Recipe = new List<Product>();
        }
         

        
    }
}
