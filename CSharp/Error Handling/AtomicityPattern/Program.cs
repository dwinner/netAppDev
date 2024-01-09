namespace AtomicityPattern;

internal static class Program
{
   private static void Main()
   {
      var a = new Accumulator();
      try
      {
         a.Add(4, 5); // a.Total is now 9
         a.Add(1, int.MaxValue); // Will cause OverflowException
      }
      catch (OverflowException)
      {
         Console.WriteLine(a.Total); // a.Total is still 9
      }
   }
}