using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace PseudoMenu.units.ru
{
    internal class UnitsRu
    {

        string milligram;
        string gram;
        string Kilogram;
        string Milliliter;
        string Liter;
        string Amount;

        internal UnitsRu() 
        {
            var russian = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = russian;

            milligram = Mass.GetAbbreviation(MassUnit.Milligram);
            gram = Mass.GetAbbreviation(MassUnit.Gram);
            Kilogram = Mass.GetAbbreviation(MassUnit.Kilogram);
            Milliliter = Volume.GetAbbreviation(VolumeUnit.Milliliter);
            Liter = Volume.GetAbbreviation(VolumeUnit.Liter);
            Amount = Scalar.GetAbbreviation(ScalarUnit.Amount);


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


            if (dimension == milligram) { quantity = Mass.FromMilligrams(value); }
            if (dimension == gram) { quantity = Mass.FromGrams(value); }
            if (dimension == Kilogram) { quantity = Mass.FromKilograms(value); }
            if (dimension == Milliliter) { quantity = Volume.FromMilliliters(value); }
            if (dimension == Liter) { quantity = Volume.FromLiters(value); }
            if(dimension == Amount) { quantity = Scalar.FromAmount(value); }

            return quantity;

        }


    }
}
