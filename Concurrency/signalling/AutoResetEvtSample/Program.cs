namespace AutoResetEvtSample;

internal static class Program
{
   private static readonly EventWaitHandle WaitHandle = new AutoResetEvent(false);

   private static void Main()
   {
      new Thread(Waiter).Start();
      Thread.Sleep(1000); // Pause for a second...
      WaitHandle.Set(); // Wake up the Waiter.
   }

   private static void Waiter()
   {
      Console.WriteLine("Waiting...");
      WaitHandle.WaitOne(); // Wait for notification
      Console.WriteLine("Notified");
   }
}