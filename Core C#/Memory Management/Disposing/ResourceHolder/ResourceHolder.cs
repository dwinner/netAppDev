using System;

namespace ResourceHolder
{
   /// <summary>
   /// Шаблон освобождения ресурсов
   /// </summary>
   public class ResourceHolder : IDisposable
   {
      private bool _isDisposed;

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      protected virtual void Dispose(bool disposing)
      {
         if (!_isDisposed)
         {
            if (disposing)
            {
               // TODO: Освобождаем управляемые объекты, путем вызова их методов Dispose()
            }

            // TODO: Освобождение неуправляемых объектов
         }

         _isDisposed = true;
      }

      ~ResourceHolder()
      {
         Dispose(false);
      }

      public void SomeMethod()
      {
         // NOTE: Убедимся в том, что объект еще не был освобожден
         if (_isDisposed)
         {
            throw new ObjectDisposedException(GetType().FullName);
         }

         // TODO: Реализация метода
      }
   }
}
