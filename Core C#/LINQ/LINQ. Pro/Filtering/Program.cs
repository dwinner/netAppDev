/**
 * Фильтрация
 */

using System;
using System.Linq;
using DataLib;

namespace Filtering
{
   static class Program
   {
      static void Main()
      {
         Filtering();
         Console.WriteLine();

         IndexFiltering();
         Console.WriteLine();

         TypeFiltering();
         Console.WriteLine();
      }

      static void Filtering() // Обычная фильтрация
      {
         var racers = from champion in Formula1.Champions
                      where champion.Wins > 15
                        && (champion.Country == "Brazil" || champion.Country == "Austria")
                      select champion;
         foreach (var racer in racers)
         {
            Console.WriteLine(racer);
         }
      }

      static void IndexFiltering()  // Фильтрация с индексом
      {
         var racers = Formula1.Champions.Where(
            (racer, index) => racer.LastName.StartsWith("A") && index % 2 != 0);
         foreach (var racer in racers)
         {
            Console.WriteLine("{0:A}", racer);
         }
      }

      static void TypeFiltering()   // Фильтрация по типу
      {
         object[] data = { "one", 2, 3, "four", "five", 6 };
         var query = data.OfType<string>();
         foreach (var s in query)
         {
            Console.WriteLine(s);
         }
      }
   }
}
