using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace PseudoMenu.units.ru
{
    internal class DimensionRU
    {

       public DimensionRU() 
        {
            var russian = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = russian;

        }
        /// <summary>
        /// принимает значение и возвращает размерность в русской культуре (для БД)
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Возвращает размерность в русской культуре (для БД)</returns>
        string dimensionRU(IQuantity t) 
        {

            return t.ToUnit(t.Unit).ToString().Split(" ")[1];


        }



    }
}
