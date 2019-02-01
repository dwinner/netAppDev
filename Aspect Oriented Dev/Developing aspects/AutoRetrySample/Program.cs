/**
 * Автоматические попытки вызова методов, предыдущие вызовы которых
 * закончились неудачно
 */

using System;
using System.Diagnostics;
using System.Net;

namespace AutoRetrySample
{
   internal static class Program
   {
      private static readonly Random Random = new Random();
      private static readonly Stopwatch Stopwatch = Stopwatch.StartNew();

      static void Main()
      {
         Console.WriteLine(DownloadFile());
      }

      [AutoRetry(MaxRetries = 3, HandledExceptions = new Type[] { typeof (WebException) }, Delay = 2.0f)]
      private static string DownloadFile()
      {
         WriteMessage("Attempting to download the file.");

         // Randomly decide if the method call should succeed or fail.
         if (Random.NextDouble() < 0.8)
         {
            // Simulate a network failure.

            WriteMessage("Network failure.");
            throw new WebException();
         }
         // Simulate success.
         WriteMessage("Success!");
         return "Hello, world.";
      }

      // Writes a message to the console with a timestamp.
      private static void WriteMessage(string message)
      {
         Console.WriteLine("{0} ms - {1}", Stopwatch.ElapsedMilliseconds, message);
      }
   }
}