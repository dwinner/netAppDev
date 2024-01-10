/**
 * Обратный итератор
 */

using System;
using System.Collections.Generic;

namespace _13_ReverseEnumerator
{
   internal class Program
   {
      private static void Main()
      {
         var intList = new List<int> { 1, 2, 3, 4 };
         foreach (var n in CreateReverseIterator(intList))
         {
            Console.WriteLine(n);
         }

         Console.ReadKey();
      }

      private static IEnumerable<T> CreateReverseIterator<T>(IList<T> list)
      {
         var count = list.Count;
         for (var i = count - 1; i >= 0; --i)
         {
            yield return list[i];
         }
      }
   }
}