using System;
using System.Runtime.InteropServices;

namespace SpanBehaviors
{
   internal static class Program
   {
      private static void Main()
      {
         SpanOnTheHeap();
         SpanOnTheStack();
         SpanOnNativeMemory();
         SpanExtensions();
      }

      private static void SpanOnTheHeap()
      {
         Console.WriteLine(nameof(SpanOnTheHeap));
         var span1 = new[] {1, 5, 11, 71, 22, 19, 21, 33}.AsSpan();
         span1.Slice(4, 3).Fill(42);
         Console.WriteLine(string.Join(", ", span1.ToArray()));
         Console.WriteLine();
      }

      private static unsafe void SpanOnTheStack()
      {
         Console.WriteLine(nameof(SpanOnTheStack));

         const int arraySize = 20;
         var lp = stackalloc long[arraySize];
         var span1 = new Span<long>(lp, arraySize);

         for (var i = 0; i < arraySize; i++)
         {
            span1[i] = i;
         }

         Console.WriteLine(string.Join(", ", span1.ToArray()));
         Console.WriteLine();
      }

      private static unsafe void SpanOnNativeMemory()
      {
         Console.WriteLine(nameof(SpanOnNativeMemory));

         const int nBytes = 100;
         var p = Marshal.AllocHGlobal(nBytes);
         try
         {
            var p2 = (int*) p.ToPointer();
            var span = new Span<int>(p2, nBytes >> 2);
            span.Fill(42);

            var max = nBytes >> 2;
            for (var i = 0; i < max; i++)
            {
               Console.Write($"{span[i]} ");
            }

            Console.WriteLine();
         }
         finally
         {
            Marshal.FreeHGlobal(p);
         }

         Console.WriteLine();
      }

      private static void SpanExtensions()
      {
         Console.WriteLine(nameof(SpanExtensions));

         var span1 = new[] {1, 5, 11, 71, 22, 19, 21, 33}.AsSpan();
         var span2 = span1.Slice(3, 4);
         var overlaps = span1.Overlaps(span2);
         Console.WriteLine($"span1 overlaps span2: {overlaps}");
         span1.Reverse();
         Console.WriteLine($"span1 reversed: {string.Join(", ", span1.ToArray())}");
         Console.WriteLine($"span2 (a slice) after reversing span1: {string.Join(", ", span2.ToArray())}");
         var index = span1.IndexOf(span2);
         Console.WriteLine($"index of span2 in span1: {index}");
         Console.WriteLine();
      }
   }
}