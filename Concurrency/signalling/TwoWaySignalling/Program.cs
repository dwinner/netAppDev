namespace TwoWaySignalling;

internal static class Program
{
   private static readonly EventWaitHandle Ready = new AutoResetEvent(false);
   private static readonly EventWaitHandle Go = new AutoResetEvent(false);
   private static readonly object Locker = new();
   private static string? _message;

   private static void Main()
   {
      new Thread(Work).Start();

      Ready.WaitOne(); // First wait until worker is ready
      lock (Locker)
      {
         _message = "ooo";
      }

      Go.Set(); // Tell worker to go

      Ready.WaitOne();
      lock (Locker)
      {
         _message = "ahhh"; // Give the worker another message
      }

      Go.Set();

      Ready.WaitOne();
      lock (Locker)
      {
         _message = null; // Signal the worker to exit
      }

      Go.Set();
   }

   private static void Work()
   {
      while (true)
      {
         Ready.Set(); // Indicate that we're ready
         Go.WaitOne(); // Wait to be kicked off...
         lock (Locker)
         {
            if (_message == null)
            {
               return; // Gracefully exit
            }

            Console.WriteLine(_message);
         }
      }
   }
}