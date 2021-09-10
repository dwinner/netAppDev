using System;

namespace ResourceHolder
{
   public abstract class DisposeRoutine : IDisposable
   {
      private bool IsDisposed { get; set; }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      ~DisposeRoutine()
      {
         Dispose(false);
      }

      private void Dispose(bool disposing)
      {
         if (!IsDisposed)
         {
            if (disposing)
            {
               ManagedCleaning();
            }

            UnmanagedCleaning();
         }

         IsDisposed = true;
      }

      protected abstract void UnmanagedCleaning();

      protected abstract void ManagedCleaning();
   }
}