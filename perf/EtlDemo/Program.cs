using System;
using System.Diagnostics.Tracing;

namespace EtlDemo
{
   internal static class Program
   {
      private static void Main()
      {
         var consoleListener = new ConsoleListener(
            new[]
            {
               new SourceConfig
               {
                  Name = "EtlDemo",
                  Level = EventLevel.Informational,
                  Keywords = Events.Keywords.General
               }
            });

         var fileListener = new FileListener(
            new[]
            {
               new SourceConfig
               {
                  Name = "EtlDemo",
                  Level = EventLevel.Verbose,
                  Keywords = Events.Keywords.PrimeOutput
               }
            },
            "PrimeOutput.txt");

         long start = 1_000_000;
         long end = 10_000_000;
         Events._Log.NullString("This won't be logged");
         Events._Log.ProcessingStart();
         for (var i = start; i < end; i++)
         {
            if (IsPrime(i))
            {
               Events._Log.FoundPrime(i);
            }
         }

         Events._Log.ProcessingFinish();
         consoleListener.Dispose();
         fileListener.Dispose();
      }

      private static bool IsPrime(long number)
      {
         if (number % 2 == 0)
         {
            if (number == 2)
            {
               return true;
            }

            return false;
         }

         var sqrt = (long)Math.Sqrt(number);
         for (var i = 3; i <= sqrt; i += 2)
         {
            if (number % i == 0)
            {
               return false;
            }
         }

         return true;
      }
   }
}