using System;

namespace CleanUpTemplate
{
   /// <summary>
   /// Формализованный шаблон очистки объекта
   /// </summary>
   class ResourceWrapper : IDisposable
   {
      private bool disposed = false;   // Выполнялась ли очистка

      public void Dispose()
      {
         CleanUp();
         GC.SuppressFinalize(this);  // Явно подавляем финализацию
         Console.WriteLine("Dispose for {0}", this);
      }

      /// <summary>
      /// Скрытое управление очисткой
      /// </summary>
      /// <param name="disposing">
      /// true, если нужно чистить управляемые ресурсы, false, если мы полагаемся на GC
      /// </param>
      private void CleanUp(bool disposing = true)
      {
         if (!disposed)
         {
            if (disposing)
            {
               // TODO: Освобождаем управляемые ресурсы.
            }
            // TODO: Освобождаем неуправляемые ресурсы.
            disposed = true;
         }
      }

      ~ResourceWrapper()
      {
         CleanUp(false);
         Console.Beep();
      }
   }
}
