#nullable disable

namespace RwLock_Upgrade;

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

      new Thread(Write).Start("A");
      new Thread(Write).Start("B");
   }

   private static void Read()
   {
      while (true)
      {
         try
         {
            Rw.EnterReadLock();
            foreach (var _ in Items)
            {
               Thread.Sleep(10);
            }
         }
         finally
         {
            Rw.ExitReadLock();
         }
      }
   }

   private static void Write(object threadId)
   {
      while (true)
      {
         var newNumber = GetRandNum(100);
         try
         {
            Rw.EnterUpgradeableReadLock();
            if (!Items.Contains(newNumber))
            {
               try
               {
                  Rw.EnterWriteLock();
                  Items.Add(newNumber);
                  Console.WriteLine($"Thread {threadId} added {newNumber}");
               }
               finally
               {
                  Rw.ExitWriteLock();
               }
            }
         }
         finally
         {
            Rw.ExitUpgradeableReadLock();
         }

         Thread.Sleep(100);
      }
   }

   private static int GetRandNum(int max)
   {
      lock (Rand)
      {
         return Rand.Next(max);
      }
   }
}