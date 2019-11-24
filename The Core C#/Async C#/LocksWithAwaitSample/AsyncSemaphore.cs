using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocksWithAwaitSample
{
   public class AsyncSemaphore
   {
      private readonly SemaphoreSlim _semaphore;

      public AsyncSemaphore() => _semaphore = new SemaphoreSlim(1);

      public async Task<IDisposable> WaitAsync(bool continueOnCaptured = false)
      {
         await _semaphore.WaitAsync().ConfigureAwait(continueOnCaptured);
         return new SemaphoreReleaser(_semaphore);
      }

      private sealed class SemaphoreReleaser : IDisposable
      {
         private readonly SemaphoreSlim _semaphore;

         public SemaphoreReleaser(SemaphoreSlim semaphore) => _semaphore = semaphore;

         public void Dispose() => _semaphore?.Release();
      }
   }
}