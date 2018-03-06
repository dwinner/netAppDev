/**
 * Упорядочивание объектов в контейнерных типах
 */

using System;
using System.Collections.Generic;
using static System.Console;

namespace ComparableObjects
{
   internal static class Program
   {
      private static ComplexNumber[] SampleNumbers
         => new[] {new ComplexNumber(5, 6), new ComplexNumber(4, 5), new ComplexNumber(3, 4), new ComplexNumber(2, 3)};

      private static void Main()
      {
         UsingDefaultSortingStrategy();
         UsingCustomComparer();
         UsingFunctionalComparer();
         UsingEqualityComparerInSets();

         ReadKey();
      }

      private static void PrintArray<T>(IEnumerable<T> complexNumbers)
         where T : ComplexNumber
      {
         foreach (var complexNumber in complexNumbers)
         {
            WriteLine("Magnitude: {0}. Real part = {1}. Img part = {2}",
               complexNumber.ComputeMagnitude(),
               complexNumber.Real,
               complexNumber.Imaginary);
         }
      }

      private static void UsingDefaultSortingStrategy()
      {
         var numbers = SampleNumbers;
         WriteLine("Before sort:");
         PrintArray(numbers);
         Array.Sort(numbers);
         WriteLine("After sort:");
         PrintArray(numbers);
      }

      private static void UsingCustomComparer()
      {
         var numbers = SampleNumbers;
         WriteLine("Before sort:");
         PrintArray(numbers);
         Array.Sort(numbers, ComplexNumber.DefaultComparer);
         WriteLine("After sort:");
         PrintArray(numbers);
      }

      private static void UsingFunctionalComparer()
      {
         var numbers = new List<ComplexNumber>(SampleNumbers);
         WriteLine("Before sort:");
         PrintArray(numbers);
         numbers.Sort(ComplexNumber.DefaultComparison);
         WriteLine("After sort:");
         PrintArray(numbers);
      }

      private static void UsingEqualityComparerInSets()
      {
         var numbers = new HashSet<ComplexNumber>(SampleNumbers, ComplexNumber.DefaultEqualityComparer);
         PrintArray(numbers);
      }
   }
}