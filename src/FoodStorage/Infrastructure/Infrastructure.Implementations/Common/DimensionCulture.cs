using System.Globalization;
using UnitsNet.Units;
using UnitsNet;

namespace FoodStorage.Infrastructure.Implementations.Common;

internal static class DimensionCulture
{
    public static string MilligramRu { get; private set; }
    public static string GramRu { get; private set; }
    public static string KilogramRu { get; private set; }
    public static string MilliliterRu { get; private set; }
    public static string LiterRu { get; private set; }
    public static string AmountRu { get; private set; }

    public static string MilligramEn { get; private set; }
    public static string GramEn { get; private set; }
    public static string KilogramEn { get; private set; }
    public static string MilliliterEn { get; private set; }
    public static string LiterEn { get; private set; }
    public static string AmountEn { get; private set; }

    static DimensionCulture()
    {
        SetDimensionRu();
        SetDimensionEn();
    }

    static private void SetDimensionRu()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");

        MilligramRu = Mass.GetAbbreviation(MassUnit.Milligram);
        GramRu = Mass.GetAbbreviation(MassUnit.Gram);
        KilogramRu = Mass.GetAbbreviation(MassUnit.Kilogram);
        MilliliterRu = Volume.GetAbbreviation(VolumeUnit.Milliliter);
        LiterRu = Volume.GetAbbreviation(VolumeUnit.Liter);
        AmountRu = Scalar.GetAbbreviation(ScalarUnit.Amount);
    }

    static private void SetDimensionEn()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-EN");

        MilligramEn = Mass.GetAbbreviation(MassUnit.Milligram);
        GramEn = Mass.GetAbbreviation(MassUnit.Gram);
        KilogramEn = Mass.GetAbbreviation(MassUnit.Kilogram);
        MilliliterEn = Volume.GetAbbreviation(VolumeUnit.Milliliter);
        LiterEn = Volume.GetAbbreviation(VolumeUnit.Liter);
        AmountEn = Scalar.GetAbbreviation(ScalarUnit.Amount);
    }
}
