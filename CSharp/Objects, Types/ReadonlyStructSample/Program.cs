using System;

namespace ReadonlyStructSample
{
   internal static class Program
   {
      private static void Main()
      {
         var point = new Dimensions(3, 6);
         Console.WriteLine(point.Diagonal);
      }
   }
}