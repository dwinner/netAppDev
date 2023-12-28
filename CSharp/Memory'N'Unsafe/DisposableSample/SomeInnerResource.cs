using System;

public class SomeInnerResource : IDisposable
{
   private bool _disposedValue; // To detect redundant calls

   public SomeInnerResource() =>
      Console.WriteLine("simulation to allocate native memory");

   // This code added to correctly implement the disposable pattern.
   public void Dispose()
   {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      GC.SuppressFinalize(this);
   }

   public void Foo()
   {
      if (_disposedValue)
      {
         throw new ObjectDisposedException(nameof(SomeInnerResource));
      }

      Console.WriteLine($"{nameof(SomeInnerResource)}.{nameof(Foo)}");
   }

   protected virtual void Dispose(bool disposing)
   {
      if (!_disposedValue)
      {
         if (disposing)
         {
            // TODO: dispose managed state (managed objects).
         }

         // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
         Console.WriteLine("simulation to release native memory");
         // TODO: set large fields to null.

         _disposedValue = true;
      }
   }

   // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
   ~SomeInnerResource() => Dispose(false);
}