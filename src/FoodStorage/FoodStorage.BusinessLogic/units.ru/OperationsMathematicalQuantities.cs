using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace PseudoMenu.units.ru
{
    internal class OperationsMathematicalQuantities
    {
        /// <summary>
        /// Суммирует знаяения разных размерностей 
        /// </summary>
        /// <param name="quantity1"></param>
        /// <param name="quantity2"></param>
        /// <returns>Возвращает значение суммы с с размерностью одного из операндов размерность которого наибольше </returns>
        /// <exception cref="ArgumentException"></exception>
        internal IQuantity sum(IQuantity quantity1 , IQuantity quantity2) 
        {

            IQuantity quantity= default;

            if (quantity1.QuantityInfo.Name == quantity2.QuantityInfo.Name)
            {

                int g1 = quantity1.Unit.GetHashCode();

                if (quantity1.Unit.GetHashCode() < quantity2.Unit.GetHashCode())
                {
                    double sumValue = ((double)quantity1.Value) + ((double)quantity2.ToUnit(quantity1.Unit).Value);
                    quantity = Quantity.From(sumValue, quantity1.Unit);
                }

                else
                {
                    double sumValue = ((double)quantity2.Value) + ((double)quantity1.ToUnit(quantity2.Unit).Value);
                    quantity = Quantity.From(sumValue, quantity2.Unit);
                }

            }

            else { throw new ArgumentException("неправильные аргументы функции"); };

            return quantity;
        
        }

    }
}
