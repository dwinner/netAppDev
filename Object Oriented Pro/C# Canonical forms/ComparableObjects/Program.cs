/**
 * Упорядочивание объектов в контейнерных типах
 */

using System;
using System.Collections.Generic;

namespace ComparableObjects
{
   internal static class Program
   {
      private static void Main()
      {
         var number1 = new ComplexNumber(2, 3);
         var number2 = new ComplexNumber(3, 4);
         var number3 = new ComplexNumber(4, 5);
         var number4 = new ComplexNumber(5, 6);

         var complexNumbers = new[]
         {
            number4,
            number3,
            number2,
            number1
         };

         Console.WriteLine("Before sort:");
         PrintArray(complexNumbers);
         Array.Sort(complexNumbers); // Note: сравнение проходит по умолчанию
         Console.WriteLine("After sort:");
         PrintArray(complexNumbers);

         Console.ReadKey();
      }

      private static void PrintArray<T>(IEnumerable<T> complexNumbers)
      {
         foreach (var complexNumber in complexNumbers)
         {
            Console.WriteLine(complexNumber);
         }
      }
   }
}