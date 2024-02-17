namespace LowLevelProducerConsumer;

internal static class Program
{
   private static void Main()
   {
      var q = new PcQueue(2);

      Console.WriteLine("Enqueuing 10 items...");

      for (var i = 0; i < 10; i++)
      {
         var localI = i; // To avoid the captured variable trap
         q.EnqueueItem(() =>
         {
            Thread.Sleep(1000); // Simulate time-consuming work
            Console.Write($" Task{localI}");
         });
      }

      q.Shutdown(true);
      Console.WriteLine();
      Console.WriteLine("Workers complete!");
   }
}