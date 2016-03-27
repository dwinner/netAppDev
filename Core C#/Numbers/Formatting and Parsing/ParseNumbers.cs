using System;
using System.Globalization;

namespace HowToCSharp.Ch05.FormattingAndParsing
{
   class ParseNumbers
   {
      public static void Run()
      {
         // Попытка преобразования --ok
         const string goodStr = " -100,000,000.567 ";
         double goodVal;
         if (double.TryParse(goodStr, out goodVal))
         {
            Console.WriteLine("Parsed {0} to number {1}", goodStr, goodVal);
         }

         // Попытка преобразования -- с десятичным разделителем
         if (!double.TryParse(goodStr, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out goodVal))
         {
            Console.WriteLine("Unable to parse {0} with limited NumberStyle", goodStr);
         }

         // Неудачная попытка преобразования
         const string badStr = "-100 100 100,987";
         double badVal;
         if (!double.TryParse(badStr, out badVal))
         {
            Console.WriteLine("Unable to parse {0} into number", badStr);
         }

         // Попытка преобразования с учетом региональных стандартов
         const string frStr = "-100 100 100,987";
         double frVal;
         if (double.TryParse(frStr, NumberStyles.Any, CultureInfo.CreateSpecificCulture("fr-FR"), out frVal))
         {
            Console.WriteLine("Parsed {0} ({1}) into {2}", frStr, "fr-FR", frVal);
         }

         // Попытка преобразования шестнадцатиричного числа
         const string hexStr = "0x3039";
         Int32 hexVal;
         if (Int32.TryParse(hexStr.Replace("0x", string.Empty), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out hexVal))
         {
            Console.WriteLine("Parsed {0} to value {1}", hexStr, hexVal);
         }

      }
   }
}
