using System.Globalization;
using UnitsNet;

namespace Infrastructure.Implementations
{
    internal class OperationsMathematicalQuantities
    {

        static OperationsMathematicalQuantities() { }

        /// <summary>
        /// принимает значение и размерность из БД и  возвращает тип IQuantity со значением и размерностью
        /// </summary>
        /// <param name="value">Значение </param>
        /// <param name="dimension">размерность в русской культуре</param>
        /// <returns></returns>
        internal static IQuantity DataUnits(double value, string dimension)
        {
            IQuantity quantity = default;
            if (dimension == DemensionCulture.MilligramRu || dimension == DemensionCulture.MilligramEn) { quantity = Mass.FromMilligrams(value); }
            if (dimension == DemensionCulture.GramRu || dimension == DemensionCulture.GramEn) { quantity = Mass.FromGrams(value); }
            if (dimension == DemensionCulture.KilogramRu || dimension == DemensionCulture.KilogramEn) { quantity = Mass.FromKilograms(value); }
            if (dimension == DemensionCulture.MilliliterRu || dimension == DemensionCulture.MilliliterEn) { quantity = Volume.FromMilliliters(value); }
            if (dimension == DemensionCulture.LiterRu || dimension == DemensionCulture.LiterEn) { quantity = Volume.FromLiters(value); }
            if (dimension == DemensionCulture.AmountRu || dimension == DemensionCulture.AmountEn) { quantity = Scalar.FromAmount(value); }
            return quantity;
        }

        /// <summary>
        /// принимает значение и возвращает размерность в русской культуре
        /// </summary>
        /// <param name="Value">значение IQuantity</param>
        /// <returns>Возвращает размерность в русской культуре (для БД)</returns>
        internal static string DimensionValueCultureRU(IQuantity Value)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU"); 
            return Value.ToUnit(Value.Unit).ToString().Split(" ")[1];
        }

        /// <summary>
        /// принимает значение и возвращает размерность в анлийской культуре
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        internal static string DimensionValueCultureEN(IQuantity Value)
        {
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-En");
            return Value.ToUnit(Value.Unit).ToString().Split(" ")[1];
        }

        /// <summary>
        /// Вспомогательная функция для функции Sum
        /// </summary>
        /// <param name="quantity1"></param>
        /// <param name="quantity2"></param>
        /// <returns>взвращает сумму значений приведенного к размерности 1 операнда</returns>
        static private IQuantity SumLocalize(IQuantity quantity1, IQuantity quantity2)
        {
            IQuantity quantity = default;
            double sumValue = ((double)quantity1.Value) + ((double)quantity2.ToUnit(quantity1.Unit).Value);
            quantity = Quantity.From(sumValue, quantity1.Unit);
            return quantity;
        }

        /// <summary>
        /// Суммирует 2 знаяения разных размерностей 
        /// </summary>
        /// <param name="quantity1"></param>
        /// <param name="quantity2"></param>
        /// <returns>Возвращает значение суммы с размерностью одного из операндов размерность которого наибольшая </returns>
        /// <exception cref="ArgumentException"></exception>
        static internal IQuantity Sum(IQuantity quantity1, IQuantity quantity2)
        {
            IQuantity quantity = default;
            if (quantity1.QuantityInfo.Name == quantity2.QuantityInfo.Name)
            {
                if (quantity1.Unit.GetHashCode() < quantity2.Unit.GetHashCode())
                {
                    quantity = SumLocalize(quantity1, quantity2);
                }
                else
                {
                    quantity = SumLocalize(quantity2, quantity1);
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
        static internal IQuantity Difference(IQuantity quantity1, IQuantity quantity2)
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
                        double differenceValue = (((double)quantity2.ToUnit(quantity1.Unit).Value - (double)quantity1.Value));
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
            else { throw new ArgumentException("Неправильные аргументы функции"); };
            return quantity;
        }

    }
}
