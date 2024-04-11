using System.Globalization;
using UnitsNet.Units;
using UnitsNet;

namespace Infrastructure.Implementations
{
    internal class DemensionCulture
    {
        public static string MilligramRu;
        public static string GramRu;
        public static string KilogramRu;
        public static string MilliliterRu;
        public static string LiterRu;
        public static string AmountRu;

        public static string MilligramEn;
        public static string GramEn;
        public static string KilogramEn;
        public static string MilliliterEn;
        public static string LiterEn;
        public static string AmountEn;

        static DemensionCulture(){ Demension();}

        static private void Demension()
        {
            var russian = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = russian;
            MilligramRu = Mass.GetAbbreviation(MassUnit.Milligram);
            GramRu = Mass.GetAbbreviation(MassUnit.Gram);
            KilogramRu = Mass.GetAbbreviation(MassUnit.Kilogram);
            MilliliterRu = Volume.GetAbbreviation(VolumeUnit.Milliliter);
            LiterRu = Volume.GetAbbreviation(VolumeUnit.Liter);
            AmountRu = Scalar.GetAbbreviation(ScalarUnit.Amount);

            var english = new CultureInfo("en-EN");
            Thread.CurrentThread.CurrentCulture = english;
            MilligramEn = Mass.GetAbbreviation(MassUnit.Milligram);
            GramEn = Mass.GetAbbreviation(MassUnit.Gram);
            KilogramEn = Mass.GetAbbreviation(MassUnit.Kilogram);
            MilliliterEn = Volume.GetAbbreviation(VolumeUnit.Milliliter);
            LiterEn = Volume.GetAbbreviation(VolumeUnit.Liter);
            AmountEn = Scalar.GetAbbreviation(ScalarUnit.Amount);
        }
    }
}
