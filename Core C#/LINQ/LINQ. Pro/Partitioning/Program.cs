/**
 * Разделяющие операции
 */

using System;
using System.Collections.Generic;
using System.Linq;
using DataLib;

namespace Partitioning
{
   static class Program
   {
      private static readonly List<string> MetalMusic = new List<string>()
      {
         "My Dying Bride",
         "Savatage",
         "Stratovarius",
         "Anathema",
         "Masterplan",
         "Jorn",
         "Gamma Ray",
         "Axxis",
         "Warlock",
         "W.A.S.P.",
         "Kreator",
         "Pyogenesis",
         "Blind Guardian",
         "Sinner",
         "Primal Fear",
         "Gotthard",
         "Iced Earth",
         "Slayer",
         "Crematory",
         "Ancient",
         "Limbonic Art",
         "Fear Factory",
         "Sentenced",
         "Benediction",
         "Darkthrone",
         "Burzum",
      };

      static void Main()
      {
         SimplePartitioning();
         Console.WriteLine();

         SkippingCondition();
         Console.WriteLine();

         TakingCondition();
         Console.WriteLine();
      }

      private static void SimplePartitioning()  // Простое разделение
      {
         const int pageSize = 5;
         var numberPages = (int)Math.Ceiling(Formula1.Champions.Count() / (double)pageSize);

         for (int page = 0; page < numberPages; page++)
         {
            Console.WriteLine("Page {0}", page);

            var racersPerPage = (from racer in Formula1.Champions
                                 orderby racer.LastName, racer.FirstName
                                 select string.Format("{0} {1}", racer.FirstName, racer.LastName)).Skip(page * pageSize).Take(pageSize);
            foreach (var racer in racersPerPage)
            {
               Console.WriteLine(racer);
            }
            Console.WriteLine();
         }
      }

      private static void SkippingCondition()   // Пропустить элементы, начинающиеся с буквы A
      {
         foreach (var s in MetalMusic.OrderBy(s => s).SkipWhile((str, index) => str.StartsWith("A")))
         {
            Console.WriteLine(s);
         }
      }

      private static void TakingCondition()  // Взять элементы, насинающиеся с буквы A
      {
         foreach (var source in MetalMusic.OrderBy(s => s).TakeWhile((s, i) => s.StartsWith("A")))
         {
            Console.WriteLine(source);
         }
      }
   }
}
