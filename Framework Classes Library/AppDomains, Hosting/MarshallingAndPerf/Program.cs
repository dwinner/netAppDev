// Производительность при междоменных вызовах

using System;
using static System.Console;
using static System.Diagnostics.Stopwatch;

namespace MarshallingAndPerf
{
   internal static class Program
   {
      private static void Main()
      {
         const int count = 10000000;
         var nonMbro = new NonMbro();
         var m = new Mbro();
         var time = GetTimestamp();
         for (var c = 0; c < count; c++)
         {
            nonMbro.X++;
         }
         WriteLine("{0:N0}", GetTimestamp() - time);

         time = GetTimestamp();
         for (var c = 0; c < count; c++)
         {
            m.X++;
         }
         WriteLine("{0:N0}", GetTimestamp() - time);
      }

      private sealed class NonMbro
      {
         public int X;
      }

      private sealed class Mbro : MarshalByRefObject
      {
         public int X;
      }
   }
}