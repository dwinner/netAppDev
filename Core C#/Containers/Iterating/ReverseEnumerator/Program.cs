/**
 * Обратный итератор
 */

using System;
using System.Collections.Generic;

namespace _13_ReverseEnumerator
{
   class Program
   {
      static void Main()
      {
         List<int> intList = new List<int> { 1, 2, 3, 4 };
         foreach (int n in CreateReverseIterator(intList))
         {
            Console.WriteLine(n);
         }

         Console.ReadKey();
      }

      static IEnumerable<T> CreateReverseIterator<T>(IList<T> list)
      {
         int count = list.Count;
         for (int i = count - 1; i >= 0; --i)
         {
            yield return list[i];
         }
      }
   }
}
