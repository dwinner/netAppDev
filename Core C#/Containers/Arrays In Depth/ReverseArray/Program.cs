/**
 * Изменение порядка элементов массива на обратный
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace ReverseArray
{
   class Program
   {
      static void Main()
      {
         int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
         PrintArray(array);
         Reverse(array);
         PrintArray(array);

         // Note: Также можно воспользоваться методом расширения
         IEnumerable<int> reversed = array.Reverse();
         foreach (var i in reversed)
         {
            Console.Write(i + " ");
         }
         Console.WriteLine();

         PrintArray(array);   // Note: Но исходный массив не изменится

         Console.ReadKey();
      }

      private static void PrintArray<T>(IEnumerable<T> array)
      {
         foreach (T val in array)
         {
            Console.Write(val + " ");
         }
         Console.WriteLine();
      }

      private static void Reverse<T>(IList<T> array)
      {
         int left = 0, right = array.Count - 1;
         while (left < right)
         {
            T temp = array[left];
            array[left] = array[right];
            array[right] = temp;
            left++;
            right--;
         }
      }
   }
}
