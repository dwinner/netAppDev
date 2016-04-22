using System;

namespace WerDisable
{
   public sealed class ErrorModeManager : IDisposable
   {
      private const ErrorModes DefaultErrorMode = ErrorModes.SystemDefault;
      private readonly ErrorModes _oldErrorMode;
      private bool _isDisposed;

      public ErrorModeManager()
         : this(DefaultErrorMode)
      {
      }

      public ErrorModeManager(ErrorModes errorMode)
      {
         _oldErrorMode = Unmanaged.SetErrorMode(errorMode);
      }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      ~ErrorModeManager()
      {
         Dispose(false);
      }

      private void Dispose(bool disposing)
      {
         if (!_isDisposed)
         {
            if (disposing)
            {
               // NOTE: Освободить управляемые объекты
            }

            Unmanaged.SetErrorMode(_oldErrorMode);
         }

         _isDisposed = true;
      }
   }
}