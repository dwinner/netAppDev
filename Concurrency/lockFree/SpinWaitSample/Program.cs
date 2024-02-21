internal static class Program
{
   private static bool _proceed;

   public static void Main()
   {
      var task = Task.Factory.StartNew(Test);
      Thread.Sleep(1000);
      _proceed = true;
      task.Wait();
   }

   private static void Test()
   {
      SpinWait.SpinUntil(() =>
      {
         Thread.MemoryBarrier();
         return _proceed;
      });
      Console.WriteLine("Done!");
   }
}