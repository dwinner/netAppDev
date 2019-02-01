/**
 * Простеший пример кэширования
 */

namespace CustomCachingSample
{
   using System;
   using System.Diagnostics;
   using System.Threading;

   internal static class Program
   {
      private static readonly Stopwatch _Stopwatch = Stopwatch.StartNew();

      private static void Main()
      {
         // Первый вызов кэшированных методов. В данной точке кэш пуст и выполнение будет медленным.
         WriteMessage(Hello("world"));
         WriteMessage(Hello("universe"));

         // Второй вызов кэшированного метода. Результаты уже закешированы
         WriteMessage(Hello("world"));
         WriteMessage(Hello("universe"));
      }

      private static void WriteMessage(string message)
         => Console.WriteLine("{0} ms - {1}", _Stopwatch.ElapsedMilliseconds, message);

      [Cache]
      private static string Hello(string who)
      {
         WriteMessage($"Doing complex stuff for {who}.");
         Thread.Sleep(500);

         return $"Hello, {who}";
      }
   }
}