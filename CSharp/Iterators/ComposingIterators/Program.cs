namespace ComposingIterators;

internal static class Program
{
   private static void Main()
   {
      // Iterators are highly composable:

      foreach (var fib in EvenNumbersOnly(Fibs(6)))
      {
         Console.WriteLine(fib);
      }
   }

   private static IEnumerable<int> Fibs(int fibCount)
   {
      for (int i = 0, prevFib = 1, curFib = 1;
           i < fibCount;
           i++)
      {
         yield return prevFib;
         var newFib = prevFib + curFib;
         prevFib = curFib;
         curFib = newFib;
      }
   }

   private static IEnumerable<int> EvenNumbersOnly(IEnumerable<int> sequence)
   {
      foreach (var x in sequence)
      {
         if (x % 2 == 0)
         {
            yield return x;
         }
      }
   }
}