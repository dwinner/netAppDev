namespace SafeSharedState;

internal static class Program
{
   private static void Main()
   {
      ThreadSafe.Run();
   }
}

internal static class ThreadSafe
{
   private static bool _done;
   private static readonly object Locker = new();

   public static void Run()
   {
      var thread = new Thread(Go);
      thread.Start();
      Go();
   }

   private static void Go()
   {
      lock (Locker)
      {
         if (!_done)
         {
            Console.WriteLine("Done");
            _done = true;
         }
      }
   }
}