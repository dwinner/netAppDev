// Трассировка методов с инициализацией вызовов во время выполнения

using System;

namespace TracingMethodExecution
{
   internal static class Program //
   {
      private static void Main()
      {
         Go();
      }

      [TraceAspect("Performance Measurement")]
      private static void Go()
      {
         for (var i = 0; i < 10e7; i++)
         {
            var newGuid = Guid.NewGuid();
            //Console.WriteLine(newGuid);
         }
      }
   }
}