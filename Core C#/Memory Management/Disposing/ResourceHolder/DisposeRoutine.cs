using System;

namespace ResourceHolder
{
   public abstract class DisposeRoutine : IDisposable
   {
      protected virtual bool IsDisposed { get; set; }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      ~DisposeRoutine()
      {
         Dispose(false);
      }

      protected virtual void Dispose(bool disposing)
      {
         if (!IsDisposed)
         {
            if (disposing)
            {
               ManagedCleaning();
            }

            UnmanagaedCleaning();
         }

         IsDisposed = true;
      }

      protected abstract void UnmanagaedCleaning();

      protected abstract void ManagedCleaning();
   }
}