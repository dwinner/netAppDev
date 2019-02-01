/**
 * Вызов нескольких методов
 */

using System;

namespace MulticastDelegate
{
   class Program
   {
      delegate void FormatNumber(double number);

      static void Main()
      {
         // Присвоить этому делегату несколько целевых методов
         FormatNumber format = FormatNumberAsCurrency;
         format += FormatNumberWithCommas;
         format += FormatNumberWithTwoPlaces;

         format(12345.6789);

         Console.ReadKey();
      }

      static void FormatNumberAsCurrency(double number)
      {
         Console.WriteLine("A Currency: {0:C}", number);
      }

      static void FormatNumberWithCommas(double number)
      {
         Console.WriteLine("With Commas: {0:N}", number);
      }

      static void FormatNumberWithTwoPlaces(double number)
      {
         Console.WriteLine("With 3 places: {0:.###}", number);
      }
   }
}