#nullable disable

namespace SemaphoreSample;

internal static class Program
{
   private static readonly SemaphoreSlim Sem = new(3); // Capacity of 3

   private static void Main()
   {
      for (var i = 1; i <= 5; i++)
      {
         new Thread(Enter).Start(i);
      }
   }

   private static void Enter(object id)
   {
      Console.WriteLine($"{id} wants to enter");
      try
      {
         Sem.Wait();
         Console.WriteLine($"{id} is in!"); // Only three threads
         Thread.Sleep(1000 * (int)id); // can be here at
         Console.WriteLine($"{id} is leaving"); // a time.
      }
      finally
      {
         Sem.Release();
      }
   }
}