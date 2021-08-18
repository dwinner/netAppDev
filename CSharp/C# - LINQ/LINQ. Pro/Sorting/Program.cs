/**
 * Сортировка
 */

using System;
using System.Linq;
using DataLib;

namespace Sorting
{
   static class Program
   {
      static void Main()
      {
         var racers = from racer in Formula1.Champions
                      orderby racer.Country, racer.LastName, racer.FirstName
                      select racer;
         foreach (var racer in racers)
         {
            Console.WriteLine(racer);
         }
      }
   }
}
