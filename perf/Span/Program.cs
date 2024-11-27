using System;
using System.Runtime.InteropServices;

namespace Span
{
   internal static class Program
   {
      private static void Main()
      {
         byte[] array = { 0, 1, 2, 3 };
         var byteSpan = new Span<byte>(array, 1, 2);
         PrintSpan(byteSpan);

         unsafe
         {
            var stackMem = stackalloc int[4];
            var intSpan = new Span<int>(stackMem, 4);
            for (var i = 0; i < intSpan.Length; i++)
            {
               intSpan[i] = 13 + i;
            }

            PrintSpan(intSpan);
         }

         unsafe
         {
            var memSize = sizeof(int) * 4;
            var hNative = Marshal.AllocHGlobal(memSize);
            var unmanagedSpan = new Span<int>(hNative.ToPointer(), 4);
            for (var i = 0; i < unmanagedSpan.Length; i++)
            {
               unmanagedSpan[i] = 100 + i;
            }

            PrintSpan(unmanagedSpan);
            Marshal.FreeHGlobal(hNative);
         }

         var subString = "NonAllocatingSubstring".AsSpan().Slice(13);
         PrintSpan(subString);
      }

      private static void PrintSpan<T>(Span<T> span)
      {
         for (var i = 0; i < span.Length; i++)
         {
            ref var val = ref span[i];
            Console.Write(val);
            if (i < span.Length - 1)
            {
               Console.Write(", ");
            }
         }

         Console.WriteLine();
      }

      private static void PrintSpan<T>(ReadOnlySpan<T> span)
      {
         for (var i = 0; i < span.Length; i++)
         {
            var val = span[i];
            Console.Write(val);
            if (i < span.Length - 1)
            {
               Console.Write(", ");
            }
         }

         Console.WriteLine();
      }
   }
}