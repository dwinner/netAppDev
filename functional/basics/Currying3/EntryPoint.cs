/*
 * Каррирование с помощью лямбда-выражений
 */

using System;
using System.Collections.Generic;

namespace _13_Currying
{
   internal static class EntryPoint
   {
      private static void Main()
      {
         var myList = new List<double> { 1.0, 3.4, 5.4, 6.54 };
         var newList = new List<double>();

         // Исходное выражение
         Func<double, double, double> func = (x, y) => x + y;

         // Каррированная функция
         var funcBound = func.Bind2Nd(3.2);
         foreach (var item in myList)
         {
            Console.Write("{0}, ", item);
            newList.Add(funcBound(item));
         }

         Console.WriteLine();

         foreach (var item in newList)
         {
            Console.Write("{0}, ", item);
         }

         Console.ReadKey();
      }
   }
}