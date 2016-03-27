/**
 * Неявно типизированные массивы
 */

using System;

namespace _02_ImplicitlyTypedArrays
{
   class Program
   {
      static void Main(string[] args)
      {
         // Традиционный массив
         int[] conventionalArray = new int[] { 1, 2, 3 };

         // Неявно типизированный массив
         var implicitlyTypedArray = new[] { 1, 2, 3 };
         Console.WriteLine(implicitlyTypedArray.GetType());

         // Массив double
         var someNumbers = new[] { 3.1415, 1, 6 };
         Console.WriteLine(someNumbers.GetType());

         // Матрица целых чисел
         var threeByThree = new[]
            {
               new[] { 1, 2, 3 },
               new[] { 4, 5, 6 },
               new[] { 7, 8, 9 }
            };
         foreach (var i in threeByThree)
         {
            foreach (var j in i)
            {
               Console.Write("{0}, ", j);
            }
            Console.Write("\n");
         }

         Console.ReadKey(true);
      }
   }
}
