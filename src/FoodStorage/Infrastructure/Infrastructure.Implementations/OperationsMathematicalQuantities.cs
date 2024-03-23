using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace PseudoMenu.units.ru
{
    internal class OperationsMathematicalQuantities
    {

        string milligramRu;
        string gramRu;
        string kilogramRu;
        string milliliterRu;
        string literRu;
        string amountRu;

        string milligramEn;
        string gramEn;
        string kilogramEn;
        string milliliterEn;
        string literEn;
        string amountEn;

      internal  OperationsMathematicalQuantities()
        {
            var russian = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = russian;

            milligramRu = Mass.GetAbbreviation(MassUnit.Milligram);
            gramRu = Mass.GetAbbreviation(MassUnit.Gram);
            kilogramRu = Mass.GetAbbreviation(MassUnit.Kilogram);
            milliliterRu = Volume.GetAbbreviation(VolumeUnit.Milliliter);
            literRu = Volume.GetAbbreviation(VolumeUnit.Liter);
            amountRu = Scalar.GetAbbreviation(ScalarUnit.Amount);

            var english = new CultureInfo("en-EN");
            Thread.CurrentThread.CurrentCulture = english;

            milligramEn = Mass.GetAbbreviation(MassUnit.Milligram);
            gramEn = Mass.GetAbbreviation(MassUnit.Gram);
            kilogramEn = Mass.GetAbbreviation(MassUnit.Kilogram);
            milliliterEn = Volume.GetAbbreviation(VolumeUnit.Milliliter);
            literEn = Volume.GetAbbreviation(VolumeUnit.Liter);
            amountEn = Scalar.GetAbbreviation(ScalarUnit.Amount);
        }

        /// <summary>
        /// принимает значение и размерность из БД и  возвращает тип IQuantity со значением и размерностью
        /// </summary>
        /// <param name="value">Значение </param>
        /// <param name="dimension">размерность в русской культуре</param>
        /// <returns></returns>
        internal IQuantity DataUnits(double value, string dimension)
        {
            IQuantity quantity = default;
            if (dimension == milligramRu|| dimension == milligramEn) { quantity = Mass.FromMilligrams(value); }
            if (dimension == gramRu|| dimension == gramEn) { quantity = Mass.FromGrams(value); }
            if (dimension == kilogramRu || dimension == kilogramEn) { quantity = Mass.FromKilograms(value); }
            if (dimension == milliliterRu || dimension == milliliterEn) { quantity = Volume.FromMilliliters(value); }
            if (dimension == literRu || dimension == literEn) { quantity = Volume.FromLiters(value); }
            if (dimension == amountRu || dimension == amountEn) { quantity = Scalar.FromAmount(value); }
            return quantity;

        }

        /// <summary>
        /// принимает значение и возвращает размерность в русской культуре
        /// </summary>
        /// <param name="t">значение IQuantity</param>
        /// <returns>Возвращает размерность в русской культуре (для БД)</returns>
        internal string dimensionRU(IQuantity t)
        {
            var russian = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = russian;

            return t.ToUnit(t.Unit).ToString().Split(" ")[1];
        }

        /// <summary>
        /// принимает значение и возвращает размерность в анлийской культуре
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        internal string dimensionEN(IQuantity t)
        {
            var english = new CultureInfo("en-En");
            Thread.CurrentThread.CurrentCulture = english;
            return t.ToUnit(t.Unit).ToString().Split(" ")[1];
        }

        /// <summary>
        /// Суммирует 2 знаяения разных размерностей 
        /// </summary>
        /// <param name="quantity1"></param>
        /// <param name="quantity2"></param>
        /// <returns>Возвращает значение суммы с размерностью одного из операндов размерность которого наибольшая </returns>
        /// <exception cref="ArgumentException"></exception>
        internal IQuantity sum(IQuantity quantity1 , IQuantity quantity2) 
        {
            IQuantity quantity= default;

            if (quantity1.QuantityInfo.Name == quantity2.QuantityInfo.Name)
            {
                
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


        /// <summary>
        /// Разность 2 значений 
        /// </summary>
        /// <param name="quantity1"></param>
        /// <param name="quantity2"></param>
        /// <returns>Возвращает значение разности с размерностью одного из операндов размерность которого наибольшая </returns>
        /// <exception cref="ArgumentException"></exception>
        internal IQuantity difference (IQuantity quantity1, IQuantity quantity2) 
        {
            IQuantity quantity = default;
            if (quantity1.QuantityInfo.Name == quantity2.QuantityInfo.Name)
            {    
                if (quantity1.Unit.GetHashCode() < quantity2.Unit.GetHashCode())
                {
                    if (((double)quantity1.Value) > ((double)quantity2.ToUnit(quantity1.Unit).Value))
                    {
                        double differenceValue = ((double)quantity1.Value) - ((double)quantity2.ToUnit(quantity1.Unit).Value);
                        quantity = Quantity.From(differenceValue, quantity1.Unit);
                    }
                    else 
                    {
                        double differenceValue = (((double)quantity2.ToUnit(quantity1.Unit).Value -(double)quantity1.Value));
                        quantity = Quantity.From(differenceValue, quantity1.Unit);
                    }
                }
                else
                {
                    if (((double)quantity2.Value) > (double)quantity1.ToUnit(quantity2.Unit).Value)
                    {
                        double differenceValue = (double)quantity2.Value - (double)quantity1.ToUnit(quantity2.Unit).Value;
                        quantity = Quantity.From(differenceValue, quantity2.Unit);
                    }
                    else
                    {
                        double differenceValue = (double)quantity1.ToUnit(quantity2.Unit).Value - (double)quantity2.Value;
                        quantity = Quantity.From(differenceValue, quantity2.Unit);
                    } 
                }
            }
            else { throw new ArgumentException("неправильные аргументы функции"); };
            return quantity;
        }
    }
}
