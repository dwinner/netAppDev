/**
 * Групповые делегаты
 */

using System;

namespace MulticastDelegates
{
   static class Program
   {
      static void Main()
      {
         Action<double> operations = MathOperations.MultiplyByTwo;
         operations += MathOperations.Square;

         ProcessAndDisplayNumber(operations, 2.0);
         ProcessAndDisplayNumber(operations, 7.94);
         ProcessAndDisplayNumber(operations, 1.1414);

         Console.WriteLine();
         Console.ReadLine();
      }

      private static void ProcessAndDisplayNumber(Action<double> action, double value)
      {
         Console.WriteLine();
         Console.WriteLine("ProcessAndDisplayNumber called with value = {0}", value);
         action(value);
      }
   }
}
