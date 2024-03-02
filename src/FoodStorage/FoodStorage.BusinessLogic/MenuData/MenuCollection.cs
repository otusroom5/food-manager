using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PseudoMenu.MenuData
{
    internal class MenuCollection
    {


        internal MenuCollection() 
        {
            menu = new Dictionary<string, Dish> (); 


        } 



        internal Dictionary<string, Dish> menu;

        internal void addMenu(Dish t) { menu.Add(t.name, t); }

    }
}
