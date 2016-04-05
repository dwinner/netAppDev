/**
 * Проблемы выбора и их решение
 */

using System;

namespace _11_DevelopmentProblems
{
   class Program
   {
      static void Main()
      {
         var c = new Complex<Int64>(3, 4, MultiplyInt64, AddInt64, Int64ToDouble, DoubleToInt64);
         Console.WriteLine("Модуль равен {0}", c.Magnitude);

         Console.ReadKey();
      }

      static Int64 MultiplyInt64(Int64 firstOp, Int64 secondOp)
      {
         return firstOp * secondOp;
      }

      static Int64 AddInt64(Int64 firstOp, Int64 secondOp)
      {
         return firstOp + secondOp;
      }

      static Int64 DoubleToInt64(double d)
      {
         return Convert.ToInt64(d);
      }

      static double Int64ToDouble(Int64 i)
      {
         return Convert.ToDouble(i);
      }
   }
}
