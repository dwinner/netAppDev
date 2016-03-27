// Захват ресурса и блокировка

using System;
using System.Threading;

namespace _05_ResourceCapture
{
   internal static class Program
   {
      private static void Main()
      {
         using (new MutexLock(new Mutex()))
         {
            // Выполнение операции безопасной в отношении потоков
         }
      }
   }

   internal struct MutexLock : IDisposable
   {
      private readonly Mutex _mutex;

      public MutexLock(Mutex mutex)
      {
         _mutex = mutex;
      }

      public void Dispose()
      {
         _mutex.ReleaseMutex();
      }
   }
}