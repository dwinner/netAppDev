namespace SpanAndStackalloc;

internal static class Program
{
   private static unsafe void Main()
   {
      Span<int> numbers = stackalloc int[1000];
      for (var i = 0; i < numbers.Length; i++)
      {
         numbers[i] = i;
      }

      var total = Sum(numbers);
      Console.WriteLine(total);
   }

   private static int Sum(ReadOnlySpan<int> numbers)
   {
      var total = 0;
      foreach (var i in numbers)
      {
         total += numbers[i];
      }

      return total;
   }
}