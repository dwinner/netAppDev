using System;
using System.Globalization;

namespace HowToCSharp.Ch05.FormattingAndParsing
{
   internal static class PrintNumbers
   {
      private static readonly CultureInfo _Ci = CultureInfo.InvariantCulture;

      public static void Run()
      {
         Console.WriteLine("Original: 12345.6789");
         PrintNumberFormatted(12345.6789);

         Console.WriteLine("");
         Console.WriteLine("Original: 0.12345");
         PrintNumberFormatted(0.12345);

         Console.WriteLine("");
         Console.WriteLine("Original: 12345");
         PrintIntNumberFormatted(12345);

         Console.WriteLine("");
         Console.WriteLine("Original: 12345");
         PrintHex(12345);

         Console.WriteLine("");
         Console.WriteLine("Currency, Original: 12345");
         Console.WriteLine("Note: some currency symbols may not show up in your console, depending on your system");
         PrintCurrencies(12345);

         Console.WriteLine("");
         Console.WriteLine("Custom Format Strings");
         PrintCustomFormatStrings(12345.6789);
      }

      private static void PrintNumberFormatted(double number)
      {
         Console.WriteLine("G: " + number.ToString("G", _Ci));
         Console.WriteLine("G4: " + number.ToString("G4", _Ci));
         Console.WriteLine("G5: " + number.ToString("G5", _Ci));

         Console.WriteLine("F: " + number.ToString("F", _Ci));
         Console.WriteLine("F6: " + number.ToString("F6", _Ci));

         Console.WriteLine("e: " + number.ToString("e", _Ci));
         Console.WriteLine("E: " + number.ToString("E", _Ci));
         Console.WriteLine("E3: " + number.ToString("E3", _Ci));

         Console.WriteLine("N: " + number.ToString("N", _Ci));
         Console.WriteLine("N0: " + number.ToString("N0", _Ci));
         Console.WriteLine("N5: " + number.ToString("N5", _Ci));

         Console.WriteLine("P: " + number.ToString("P", _Ci));
         Console.WriteLine("P1: " + number.ToString("P1", _Ci));

         Console.WriteLine("C: " + number.ToString("C", CultureInfo.CreateSpecificCulture("en-US")));
         Console.WriteLine("C3: " + number.ToString("C3", CultureInfo.CreateSpecificCulture("en-GB")));
      }

      private static void PrintIntNumberFormatted(int number)
      {
         Console.WriteLine("D: " + number.ToString("D", _Ci));
         Console.WriteLine("D8: " + number.ToString("D8", _Ci));

         Console.WriteLine("F: " + number.ToString("F", _Ci));
         Console.WriteLine("F3: " + number.ToString("F3", _Ci));

         Console.WriteLine("E: " + number.ToString("E", _Ci));
         Console.WriteLine("E3: " + number.ToString("E3", _Ci));

         Console.WriteLine("X: " + number.ToString("X", _Ci));
         Console.WriteLine("X8: " + number.ToString("X8", _Ci));
      }

      private static void PrintHex(int number)
      {
         Console.WriteLine("Hex: {0:X}", number);
         Console.WriteLine("Hex: 0x{0:X8}", number);
      }

      private static void PrintCurrencies(int number)
      {
         Console.WriteLine("en-US: " + number.ToString("C", CultureInfo.CreateSpecificCulture("en-US")));
         Console.WriteLine("en-GB: " + number.ToString("C", CultureInfo.CreateSpecificCulture("en-GB")));
         Console.WriteLine("fr-FR: " + number.ToString("C", CultureInfo.CreateSpecificCulture("fr-FR")));
         Console.WriteLine("ja-JA: " + number.ToString("C", CultureInfo.CreateSpecificCulture("ja-JA")));
         Console.WriteLine("zh-HK: " + number.ToString("C", CultureInfo.CreateSpecificCulture("zh-HK")));
         Console.WriteLine("mn-MN: " + number.ToString("C", CultureInfo.CreateSpecificCulture("mn-MN")));

         var val = 1234567.89;
         Console.WriteLine(val.ToString("N", CultureInfo.CreateSpecificCulture("fr-FR")));
         Console.WriteLine(string.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:N}", val));
      }

      private static void PrintCustomFormatStrings(double number)
      {
         Console.WriteLine(number.ToString("00000000.00", _Ci));
         Console.WriteLine(number.ToString("00,000,000.00", _Ci));
         Console.WriteLine(number.ToString("00,000,000.00", CultureInfo.CreateSpecificCulture("hi-IN")));
         var neg = number * -1;
         Console.WriteLine(neg.ToString("00,000,000.00;(00000000.000)", _Ci));
         var zero = 0.0;
         Console.WriteLine(zero.ToString("00,000,000.00;(00000000.000);'nothing!'", _Ci));
      }
   }
}