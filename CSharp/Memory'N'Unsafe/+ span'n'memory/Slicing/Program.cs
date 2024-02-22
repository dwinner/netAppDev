namespace Slicing;

internal static class Program
{
   private static void Main()
   {
      var numbers = new int[1000];
      for (var i = 0; i < numbers.Length; i++)
      {
         numbers[i] = i;
      }

      // total - using implicit conversion
      Console.WriteLine(Sum(numbers));

      // total - using .AsSpan()
      Console.WriteLine(Sum(numbers.AsSpan()));

      // total - slicing
      Console.WriteLine(Sum(numbers.AsSpan(250, 500)));

      Span<int> span = numbers;
      Console.WriteLine(span[^1]); // Last element
      Console.WriteLine(Sum(span[..10])); // First 10 elements
      Console.WriteLine(Sum(span[100..])); // 100th element to end
      Console.WriteLine(Sum(span[^5..])); // Last 5 elements
   }

   private static int Sum(ReadOnlySpan<int> numbers)
   {
      var total = 0;
      foreach (var num in numbers)
      {
         total += num;
      }

      return total;
   }
}