/**
 * Числовой эквивалент строки
 */

using System;
using System.Globalization;

namespace StrNumberEqu
{
   internal static class Program
   {
      private static void Main()
      {
         var d = char.GetNumericValue('\u0033');
         Console.WriteLine(d.ToString(CultureInfo.InvariantCulture));
         d = char.GetNumericValue('\u00bc'); // 1/4
         Console.WriteLine(d.ToString(CultureInfo.InvariantCulture));

         d = char.GetNumericValue('A'); // -1
         Console.WriteLine(d);
      }
   }
}