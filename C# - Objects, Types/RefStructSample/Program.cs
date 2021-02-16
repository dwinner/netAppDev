using System;

namespace RefStructSample
{
   internal static class Program
   {
      private static void Main()
      {
         var vt = new ValueTypeOnly(42);
         vt.AMethod();
         // vt.ToString is not allowed! 
      }
   }

   internal ref struct ValueTypeOnly
   {
      public ValueTypeOnly(int y)
      {
         Y = y;
         X = 0;
      }

      public int X;
      public int Y { get; }

      public void AMethod() => Console.WriteLine($"x: {X}, y: {Y}");
   }
}