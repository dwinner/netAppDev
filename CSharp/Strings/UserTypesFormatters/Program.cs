/**
 * Форматирование в пользовательских типах
 */

using System;
using System.Globalization;

namespace _04_UserTypesFormatters
{
   class Program
   {
      static void Main()
      {
         CultureInfo localCultureInfo = CultureInfo.CurrentCulture;
         var germanyCultureInfo = new CultureInfo("de-DE");
         var complex = new Complex(12.3456, 12.3456);
         string strCpx = complex.ToString("F", localCultureInfo);
         Console.WriteLine(strCpx);
         strCpx = complex.ToString("F", germanyCultureInfo);
         Console.WriteLine(strCpx);
         Console.WriteLine("\nОтладочный вывод:\n{0:DBG}", complex);

         Console.ReadKey();
      }
   }
}
