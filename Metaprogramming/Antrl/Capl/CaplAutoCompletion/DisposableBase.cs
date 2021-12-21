using System;

namespace CaplAutoCompletion
{
    public abstract class DisposableBase : IDisposable
    {
        protected bool IsDisposed { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableBase()
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