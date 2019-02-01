/**
 * Комбированная обработка исключений через аспекты
 */

using ExceptionHandlingSample;

// Добавляем аспект AddContextOnException ко всем методам в сборке

[assembly: AddContextOnException]

namespace ExceptionHandlingSample
{
   using System;

   internal static class Program
   {
      private static void Main()
      {
         MainCore();

         Console.WriteLine("Handled");
      }

      [ReportAndSwallowException]
      private static void MainCore()
      {
         // The Fibonacci method will fail with an exception, but the [ReportAndSwallowException] aspect
         // will swallow the exception and the MainCore method will succeed.
         Fibonacci(5);
      }

      private static int Fibonacci(int n)
      {
         if (n < 0)
         {
            throw new ArgumentOutOfRangeException();
         }

         return n == 0 ? 0 : Fibonacci(n - 1) + Fibonacci(n - 2);
      }
   }
}