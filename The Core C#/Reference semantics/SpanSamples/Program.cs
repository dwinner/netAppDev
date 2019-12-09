using System;

namespace SpanSamples
{
   internal static class Program
   {
      private static void Main()
      {
         var span1 = IntroSpans();
         var span2 = CreateSlices(span1);
         ChangeValues(span1, span2);
         ReadonlySpanSample(span2);
      }

      private static Span<int> IntroSpans()
      {
         int[] arr1 = {1, 4, 5, 11, 13, 18};
         var span1 = new Span<int>(arr1) {[1] = 11};
         Console.WriteLine($"arr1[1] is changed via span1[1]: {arr1[1]}");
         return span1;
      }

      private static Span<int> CreateSlices(Span<int> span1)
      {
         Console.WriteLine(nameof(CreateSlices));
         int[] arr2 = {3, 5, 7, 9, 11, 13, 15};
         var span2 = new Span<int>(arr2);
         var span3 = new Span<int>(arr2, 3, 3);
         var span4 = span1.Slice(2, 4);

         DisplaySpan("content of span3", span3);
         DisplaySpan("content of span4", span4);
         Console.WriteLine();

         return span2;
      }

      private static void ChangeValues(Span<int> span1, Span<int> span2)
      {
         Console.WriteLine(nameof(ChangeValues));

         var span4 = span1.Slice(4);
         span4.Clear();
         DisplaySpan("content on span1", span1);
         var span5 = span2.Slice(3, 4);
         span5.Fill(42);
         DisplaySpan("content of span2", span2);
         span5.CopyTo(span1);
         DisplaySpan("content on span1", span1);

         if (!span1.TryCopyTo(span4))
         {
            Console.WriteLine("Couldn't copy span1 to span4 because span4 is " +
                              "too small");
            Console.WriteLine($"length of span4: {span4.Length}, length of " +
                              $"span1: {span1.Length}");
         }
      }

      private static void ReadonlySpanSample(Span<int> span1)
      {
         Console.WriteLine(nameof(ReadonlySpanSample));

         var arr = span1.ToArray();
         var readOnlySpan1 = new ReadOnlySpan<int>(arr);
         DisplaySpan(nameof(readOnlySpan1), readOnlySpan1);

         ReadOnlySpan<int> readOnlySpan2 = span1;
         DisplaySpan("readOnlySpan2", readOnlySpan2);
         ReadOnlySpan<int> readOnlySpan3 = arr;
         DisplaySpan("readOnlySpan3", readOnlySpan3);
         Console.WriteLine();
      }

      private static void DisplaySpan(string title, ReadOnlySpan<int> span)
      {
         Console.WriteLine(title);
         foreach (var el in span)
         {
            Console.Write($"{el}.");
         }

         Console.WriteLine();
      }
   }
}