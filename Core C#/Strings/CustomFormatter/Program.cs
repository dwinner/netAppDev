/**
 * Собственные поставщики форматирования
 */

using System;
using System.Globalization;

namespace _05_CustomFormatter
{
   class Program
   {
      static void Main()
      {
         CultureInfo localCultureInfo = CultureInfo.CurrentCulture;
         var germanyCultureInfo = new CultureInfo("de-DE");
         var complex = new Complex(12.3456, 1234.56);
         string strCpx = complex.ToString("F", localCultureInfo);
         Console.WriteLine(strCpx);
         strCpx = complex.ToString("F", germanyCultureInfo);
         Console.WriteLine(strCpx);
         var dbgFormatter = new ComplexDbgFormatter();
         strCpx = string.Format(dbgFormatter, "{0:DBG}", complex);
         Console.WriteLine("{0}Отладочный вывод:{0}{1}", Environment.NewLine, strCpx);

         Console.ReadKey();
      }
   }
}
