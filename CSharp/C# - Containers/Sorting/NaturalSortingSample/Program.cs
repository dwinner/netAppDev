/**
 * Тестирование натуральной сортировки строк
 */

using System;
using System.Collections.Generic;

namespace SortStringsTest
{
   internal static class Program
   {
      private static void Main()
      {
         string[] originals =
            {
                "V101", "V101", "V2000", "V2008", "V505", "V501", "V56", "V2005", "V2001", "V1000",
                "V1067", "V198"
            };

         // Естественная сортировка
         Console.WriteLine("Natural sotring:");
         var copy = new List<string>(originals);
         copy.Sort(ComparerFactory.NaturalSort);
         Array.ForEach(copy.ToArray(), Console.WriteLine);
      }
   }
}