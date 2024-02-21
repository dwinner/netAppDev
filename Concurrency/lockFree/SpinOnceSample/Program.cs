namespace SpinOnceSample;

internal static class Program
{
   private static bool _proceed;

   private static void Main()
   {
      var task = Task.Run(Test);
      Thread.Sleep(1000);
      _proceed = true;
      task.Wait();
   }

   private static void Test()
   {
      var spinWait = new SpinWait();
      while (!_proceed)
      {
         Thread.MemoryBarrier();
         spinWait.SpinOnce();
      }

      Console.WriteLine("Done!");
   }
}