using System;
using System.IO;

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

   static class Test
   {
      public static void Go()
      {
         using (var routineImpl =
            new DisposeRoutineImpl(new FileStream("Test1.test", FileMode.Create), "Test2.test"))
         {
            Console.WriteLine(routineImpl);
         }
      }
   }
}
