namespace SumWithPointers;

internal static class Program
{
   private static unsafe void Main()
   {
      var numbers = stackalloc int[1_000]; // Allocate array on the stack
      for (var i = 0; i < 1_000; i++)
      {
         numbers[i] = i;
      }

      var total = Sum(numbers, 1_000);
      Console.WriteLine(total);
   }

   private static unsafe int Sum(int* numbers, int length)
   {
      var total = 0;
      for (var i = 0; i < length; i++)
      {
         total += numbers[i];
      }

      return total;
   }
}