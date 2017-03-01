using System;
using System.Diagnostics;

namespace AutoStopwatchSample
{
   public class AutoStopwatch : Stopwatch, IDisposable
   {
      public AutoStopwatch()
      {
         Start();
      }

      public void Dispose()
      {
         Stop();
         Console.WriteLine("Elapsed: {0}", Elapsed);
      }
   }
}