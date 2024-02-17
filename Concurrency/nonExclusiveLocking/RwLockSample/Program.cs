namespace RwLockSample;

internal static class Program
{
   private static readonly ReaderWriterLockSlim Rw = new();
   private static readonly List<int> Items = new();
   private static readonly Random Rand = new();

   private static void Main()
   {
      new Thread(Read).Start();
      new Thread(Read).Start();
      new Thread(Read).Start();

      new Thread(Write!).Start("A");
      new Thread(Write!).Start("B");

      Console.ReadLine();
   }

   private static void Read()
   {
      while (true)
      {
         try
         {
            Rw.EnterReadLock();
            foreach (var item in Items)
            {
               //Console.WriteLine($"Read {item}");
               Thread.Sleep(10);
            }
         }
         finally
         {
            Rw.ExitReadLock();
         }
      }
      // ReSharper disable once FunctionNeverReturns
   }

   private static void Write(object aThreadId)
   {
      while (true)
      {
         var newNumber = GetRandNum(100);
         try
         {
            Rw.EnterWriteLock();
            Items.Add(newNumber);
         }
         finally
         {
            Rw.ExitWriteLock();
         }

         Console.WriteLine($"Thread {aThreadId} added {newNumber}");
         Thread.Sleep(100);
      }
      // ReSharper disable once FunctionNeverReturns
   }

   private static int GetRandNum(int max)
   {
      lock (Rand)
      {
         return Rand.Next(max);
      }
   }
}