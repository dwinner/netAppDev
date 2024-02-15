namespace SimpleUseOfLock;

internal static class Program
{
   private static readonly object Locker = new();
   private static int _val1;
   private static int _val2;

   private static void Main()
   {
      for (var i = 1; i <= 1000; i++)
      {
         if (i % 100 == 0)
         {
            Console.WriteLine($"Tried {i} times to get DivideByZeroException");
         }

         var thread1 = new Thread(Go);
         thread1.Start();

         var thread2 = new Thread(Go);
         thread2.Start();

         var thread3 = new Thread(Go);
         thread3.Start();

         thread1.Join();
         thread2.Join();
         thread3.Join();
      }
   }

   private static void Go()
   {
      lock (Locker) // Threadsafe: will never get DivideByZeroException
      {
         if (_val2 != 0)
         {
            Console.WriteLine(_val1 / _val2);
         }

         _val2 = 0;
      }
   }
}