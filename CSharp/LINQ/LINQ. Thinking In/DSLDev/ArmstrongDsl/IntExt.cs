using System;
using System.Collections.Generic;
using System.Linq;

namespace ArmstrongDsl
{
   public static class IntExt
   {
      private static int Cube(this int number) => number * number * number;

      public static IEnumerable<int> Cube(this IEnumerable<int> digits) => digits.Select(d => Cube((int)d));

      public static int Square(this int number) => number * number;

      private static IEnumerable<int> Digits(this int number)
         => number.ToString().ToCharArray().Select(n => Convert.ToInt32(n.ToString()));

      public static IEnumerable<int> ReverseDigits(this int number) => number.Digits().Reverse();

      public static IEnumerable<int> EvenDigits(this int number)
         => number.ToString().ToCharArray().Where((m, i) => i % 2 == 0).Select(n => Convert.ToInt32(n.ToString()));

      public static IEnumerable<int> OddDigits(this int number)
         => number.ToString().ToCharArray().Where((m, i) => i%2 != 0).Select(n => Convert.ToInt32(n.ToString()));

      public static bool Are(this IEnumerable<int> actualDigits, params int[] digits)
         => actualDigits.SequenceEqual(digits);

      public static IEnumerable<int> DigitsAt(this int number, params int[] indices)
      {
         var asString = number.ToString();
         return indices.Select(i => Convert.ToInt32(asString[i].ToString()));
      }

      public static bool AreZero(this IEnumerable<int> digits) => digits.All(d => d == 0);

      public static int FormNumber(this IEnumerable<int> digits)
      {
         if (digits != null)
         {
            var ints = digits as int[] ?? digits.ToArray();
            return
               ints.Select((d, i) => d*(int) Math.Pow(10, ints.Length - (i + 1))).Aggregate((a, b) => a + b);
         }

         return 0;
      }

      public static IEnumerable<int> Factorial(this IEnumerable<int> digits)
      {
         foreach (var d in digits)
            if (d == 0)
               yield return 1;
            else
               yield return Enumerable.Range(1, d).Aggregate((a, b) => a * b);
      }

      public static int Product(this IEnumerable<int> digits) => digits.Aggregate((f, s) => f * s);
   }
}