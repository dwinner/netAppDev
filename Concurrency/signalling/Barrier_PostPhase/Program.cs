namespace Barrier_PostPhase;

internal static class Program
{
   private static readonly Barrier Barrier = new(3, barrier => Console.WriteLine());

   private static void Main()
   {
      new Thread(Speak).Start();
      new Thread(Speak).Start();
      new Thread(Speak).Start();
   }

   private static void Speak()
   {
      for (var i = 0; i < 5; i++)
      {
         Console.Write("{0} ", i);
         Barrier.SignalAndWait();
      }
   }
}