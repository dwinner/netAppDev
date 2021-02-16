using System;

namespace NumericLiteralsSample
{
   internal static class Program
   {
      private static void Main()
      {
         const int billion = 1_000_000_000;
         const int binary = 0b1010_1011_1100_1101_1110_1111;
         Console.WriteLine(binary);
         Console.WriteLine(billion);
      }
   }
}