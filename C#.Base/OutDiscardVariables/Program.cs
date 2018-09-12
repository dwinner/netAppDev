using System;

namespace OutDiscardVariables
{
   internal static class Program
   {
      private static void Main()
      {
         SampleDiscard(out _, out _, out _, out var r);
         Console.WriteLine(r);
      }

      private static void SampleDiscard(out int x, out int y, out int z, out int r)
      {
         x = 0;
         y = 0;
         z = 0;
         r = 0;
      }
   }
}