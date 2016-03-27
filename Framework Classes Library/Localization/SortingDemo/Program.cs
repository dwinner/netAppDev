/**
 * Поведение сортировки в зависимости от культуры
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace SortingDemo
{
   class Program
   {
      static void Main()
      {
         string[] names =
         {
            "Alabama",
            "Texas",
            "Washington",
            "Virginia",
            "Wisconsin",
            "Wyoming",
            "Kentucky",
            "Missouri",
            "Utah",
            "Hawaii",
            "Kansas",
            "Louisiana",
            "Alaska",
            "Arizona"
         };

         Thread.CurrentThread.CurrentCulture=new CultureInfo("fi-FI");
         Array.Sort(names);
         DisplayNames("Sorted using the Finnish culture", names);

         // Сортировать с использованием инвариантной культуры
         Array.Sort(names, Comparer.DefaultInvariant);
         DisplayNames("Sorted using the invariant culture", names);
      }

      private static void DisplayNames(string title, IEnumerable<string> e)
      {
         Console.WriteLine(title);
         foreach (var s in e)
         {
            Console.Write("{0}-", s);
         }
         Console.WriteLine();
         Console.WriteLine();
      }
   }
}
