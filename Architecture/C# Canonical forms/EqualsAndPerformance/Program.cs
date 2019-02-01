/**
 * Переопределение Equals для повышения производительности
 */

using System;

namespace EqualsAndPerformance
{
   static class Program
   {
      static void Main()
      {
         var num1 = new ComplexNumber(1, 2);
         var num2 = new ComplexNumber(1, 2);
         int cRes = num1.CompareTo(num2);
         // Попробовать безопасную к типам версию
         int compareTo = ((IComparable) num1).CompareTo(num2);
         bool result = num1.Equals(num2);
         Console.WriteLine("{0} {1} {2}", result, cRes, compareTo);
      }
   }
}
