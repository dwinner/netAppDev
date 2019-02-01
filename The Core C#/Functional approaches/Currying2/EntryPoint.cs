/**
 * Другой способ каррирования
 */

using System;
using System.Collections.Generic;

namespace _14_Currying2
{
   class EntryPoint
   {
      static void Main()
      {
         var myList = new List<double> { 1.0, 3.4, 5.4, 6.54 };
         var newList = new List<double>();

         // Исходное выражение
         Func<double, double, double> func = (x, y) => x + y;

         // Каррированная функция
         var funcBound = func.Bind2Nd()(3.2);
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

   public static class CurryExtensions
   {
      public static Func<TArg2, Func<TArg1, TResult>> Bind2Nd<TArg1, TArg2, TResult>(
         this Func<TArg1, TArg2, TResult> func)
      {
         return y => x => func(x, y);
      }
   }
}
