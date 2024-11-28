using System;
using System.Runtime.CompilerServices;

namespace JitCall
{
   internal static class Program
   {
      private static void Main()
      {
         var x = uint.MaxValue;
         var val = A();
         var val2 = A();
         Console.WriteLine(val + val2);
      }

      [MethodImpl(MethodImplOptions.NoInlining)]
      private static int A() => 42;
   }
}