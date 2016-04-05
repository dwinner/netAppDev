/**
 * Пользовательские итераторы
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_CustomIterators
{
   class Program
   {
      static void Main()
      {
         var matrix = new List<List<int>>
         {
            new List<int> {1, 2, 3},
            new List<int> {4, 5, 6},
            new List<int> {7, 8, 9}
         };
         // Более элегантный способ перечисления элементов
         foreach (var item in matrix.GetRowMajorIterator())
         {
            Console.Write("{0}, ", item);
         }

         Console.ReadKey();
      }
   }

   public static class CustomIterators
   {
      public static IEnumerable<T> GetRowMajorIterator<T>(this List<List<T>> matrix)
      {
         /*
          foreach (var row in matrix)
          {
            foreach (var item in row)
            {
               yield return item;
            }
          } 
          */
         return matrix.SelectMany(row => row);
      }
   }
}
