// Реккурентные вызовы

using System;
using System.Linq;
using MoreLinq;

namespace RecurrenceRelation
{
   internal static class Program
   {
      private static void Main()
      {
         Func<long, long, long> aRule = (x, y) => 4 * x + 5 * y;
         Func<long, long, long> fibonacciRule = (x, y) => x + y;
         Func<double, double> arbitraryRule = x => 1 / (x + 1 / x);

         var sequence1 = SequenceExt.StartWith<long>(0, 1).ThenFollow(aRule).Take(5);
         var sequence2 = SequenceExt.StartWith<long>(1, 1).ThenFollow(fibonacciRule).Take(5);
         var sequence3 = SequenceExt.StartWith(1.0).ThenFollow(arbitraryRule).Take(5);

         sequence1.ForEach(item => Console.Write("{0} ", item));
         Console.WriteLine();

         sequence2.ForEach(item => Console.Write("{0} ", item));
         Console.WriteLine();

         sequence3.ForEach(item => Console.Write("{0} ", item));
         Console.WriteLine();
      }
   }
}