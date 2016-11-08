/**
 * Различные типы протоколирования через атрибуты
 */

using System;

using CustomLoggingSample.Aspects;

// NOTE: Добавим протоколирование к любому методу в сборке

[assembly: LogMethod(AttributePriority = 0)]

// NOTE: Удаляем протоколирование из простанства имен Aspects, чтобы избежать бесконечных рекурсий

[assembly:
   LogMethod(AttributePriority = 1, AttributeExclude = true,
      AttributeTargetTypes = nameof(CustomLoggingSample.Aspects) + ".*")]

// NOTE: Добавляем протоколирование к простанству имен System.Math, чтобы показать, что мы можем добавлять протоколирование к чему угодно

[assembly:
   LogMethod(AttributePriority = 2, AttributeTargetAssemblies = "mscorlib", AttributeTargetTypes = nameof(Math))]

namespace CustomLoggingSample
{
   internal static class Program
   {
      [LogSetValue] private static int value;

      private static void Main()
      {
         value = Fibonacci(5);
         Console.WriteLine(value);

         try
         {
            Fibonacci(-1);
         }
         catch
         {
            // ignored
         }

         Console.WriteLine(Math.Sin(5));
      }

      private static int Fibonacci(int n)
      {
         if (n < 0)
         {
            throw new ArgumentOutOfRangeException();
         }

         return n == 0 ? 0 : (n == 1 ? 1 : Fibonacci(n - 1) + Fibonacci(n - 2));
      }
   }
}