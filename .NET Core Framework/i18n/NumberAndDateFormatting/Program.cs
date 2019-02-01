/**
 * Форматирование чисел и дат в различных культурах
 */

using System;
using System.Globalization;
using System.Threading;

namespace NumberAndDateFormatting
{
   class Program
   {
      static void Main()
      {
         NumberFormatDemo();
         Console.WriteLine("-----------------------------------------");
         DateTimeFormatDemo();
      }

      private static void NumberFormatDemo()
      {
         const int val = 1234567890;

         // Культура текущего потока
         Console.WriteLine(val.ToString("N"));

         // Использование IFormatProvider
         Console.WriteLine(val.ToString("N"), new CultureInfo("fr-FR"));

         // Изменение культуры потока
         Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
         Console.WriteLine(val.ToString("N"));
      }

      private static void DateTimeFormatDemo()
      {
         var dateTime = new DateTime(2012, 06, 12);

         // Текущая культура
         Console.WriteLine(dateTime.ToLongDateString());

         // Использование IFormatProvider
         Console.WriteLine(dateTime.ToString("D"), new CultureInfo("fr-FR"));

         // Использование культуры текущего потока
         CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
         Console.WriteLine("{0}: {1}", currentCulture, dateTime.ToString("D"));

         currentCulture = new CultureInfo("es-ES");
         Thread.CurrentThread.CurrentCulture = currentCulture;
         Console.WriteLine("{0}: {1}", currentCulture, dateTime.ToString("D"));
      }
   }
}
