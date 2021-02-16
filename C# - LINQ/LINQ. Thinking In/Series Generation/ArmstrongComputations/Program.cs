// Примеры вычисления некоторых математических последовательностей чисел

using System;
using System.Collections.Generic;
using System.Linq;

namespace ArmstrongComputations
{
   internal static class Program
   {
      private static void Main()
      {
         var armstrongNumbers = Enumerable.Range(0, 1000).Where(k => k.Digits().Select(x => x * x * x).Sum() == k);
         var sumProductNumbers = Enumerable.Range(0, 1000).Where(k =>
         {
            var digits = k.Digits().ToArray();
            return digits.Sum() * digits.Aggregate((x, y) => x * y) == k;
         });
         var dudeneyNumbers =
            Enumerable.Range(1, 1000).Where(e => Math.Abs(Math.Pow(e.Digits().Sum(), 3) - e) < double.Epsilon);
         var factorionNumbers =
            Enumerable.Range(1, 1000)
               .Where(
                  e =>
                     e.Digits().Where(d => d > 0).Select(x => Enumerable.Range(1, x).Aggregate((a, b) => a * b)).Sum() ==
                     e);

         Console.WriteLine("Armstrong numbers: {0}",
            armstrongNumbers.Aggregate(string.Empty,
               (current, armstrongNumber) => current + string.Format("{0} ", armstrongNumber)));
         Console.WriteLine("Product numbers: {0}",
            sumProductNumbers.Aggregate(string.Empty,
               (current, productNumber) => current + string.Format("{0} ", productNumber)));
         Console.WriteLine("Dudeney numbers: {0}",
            dudeneyNumbers.Aggregate(string.Empty,
               (current, dudeneyNumber) => current + string.Format("{0} ", dudeneyNumber)));
         Console.WriteLine("Factorion numbers: {0}",
            factorionNumbers.Aggregate(string.Empty, (current, factorion) => current + string.Format("{0} ", factorion)));
      }
   }

   public static class NumberEx
   {
      public static IEnumerable<int> Digits(this int number)
      {
         var chars = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
         return number.ToString().Select(@char => chars.IndexOf(@char)).ToList();
      }
   }
}