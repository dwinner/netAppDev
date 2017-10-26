using System;
using System.Runtime.CompilerServices;

namespace IL.Primary
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         Console.WriteLine(Square(4));
      }

      [MethodImpl(MethodImplOptions.ForwardRef)]
      public static extern int Square(int number);
   }
}