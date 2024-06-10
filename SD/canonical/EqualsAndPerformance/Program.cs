/**
 * Переопределение Equals для повышения производительности
 */

using System;

namespace EqualsAndPerformance
{
   internal static class Program
   {
      private static void Main()
      {
         var num1 = new ComplexNumber(1, 2);
         var num2 = new ComplexNumber(1, 2);
         var cRes = num1.CompareTo(num2);
         // Попробовать безопасную к типам версию
         var compareTo = ((IComparable)num1).CompareTo(num2);
         var result = num1.Equals(num2);
         Console.WriteLine("{0} {1} {2}", result, cRes, compareTo);
      }
   }
}