using System;

public class SomeResource : IDisposable
{
   private readonly SomeInnerResource _innerResource;

   public SomeResource() =>
      _innerResource = new SomeInnerResource();

   public void Foo()
   {
      if (_disposedValue)
      {
         throw new ObjectDisposedException(nameof(SomeResource));
      }

      Console.WriteLine($"{nameof(SomeResource)}.{nameof(Foo)}");
      _innerResource?.Foo();
   }

   #region IDisposable Support

   private bool _disposedValue; // To detect redundant calls

   protected virtual void Dispose(bool disposing)
   {
      if (!_disposedValue)
      {
         if (disposing)
         {
            // dispose managed state (managed objects).
            _innerResource?.Dispose();
         }

         // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
         // TODO: set large fields to null.

         _disposedValue = true;
      }
   }

   // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
   // ~SomeResource() {
   //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
   //   Dispose(false);
   // }

   // This code added to correctly implement the disposable pattern.
   public void Dispose()
   {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
   }

   #endregion
}