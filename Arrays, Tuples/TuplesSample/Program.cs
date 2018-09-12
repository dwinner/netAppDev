/**
 * Кортежи в C#
 */

using System;

namespace TuplesSample
{
   static class Program
   {
      static void Main()
      {
         var name = new Tuple<string, string>("Jochen", "Rindt");
         Console.WriteLine(name.ToString());

         var result = Divide(5, 2);
         Console.WriteLine("result of division: {0}, reminder: {1}", result.Item1, result.Item2);

         AnyElementNumber();
      }

      static void AnyElementNumber()
      {
         var tuple = Tuple.Create(
             "Stephanie", "Alina", "Nagel", 2009, 6, 2, 1.37, Tuple.Create(52, 3490));
         Console.WriteLine(tuple.Item1);
      }

      public static Tuple<int, int> Divide(int dividend, int divisor)
      {
         int result = dividend / divisor;
         int reminder = dividend % divisor;

         return Tuple.Create(result, reminder);
      }
   }
}
