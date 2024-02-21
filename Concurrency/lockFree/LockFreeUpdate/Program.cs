namespace LockFreeUpdate;

internal static class Program
{
   private static int _x = 2;

   private static void Main()
   {
      // We can perform three multiplications on the same variable using 3 concurrent threads
      // safely without locks by using SpinWait with Interlocked.CompareExchange.

      var task1 = Task.Factory.StartNew(() => MultiplyXBy(3));
      var task2 = Task.Factory.StartNew(() => MultiplyXBy(4));
      var task3 = Task.Factory.StartNew(() => MultiplyXBy(5));

      Task.WaitAll(task1, task2, task3);
      Console.WriteLine(_x);
   }

   private static void MultiplyXBy(int factor)
   {
      var spinWait = new SpinWait();
      while (true)
      {
         var snapshot1 = _x;
         Thread.MemoryBarrier();
         var calc = snapshot1 * factor;
         var snapshot2 = Interlocked.CompareExchange(ref _x, calc, snapshot1);
         if (snapshot1 == snapshot2)
         {
            return; // No one preempted us.
         }

         spinWait.SpinOnce();
      }
   }
}