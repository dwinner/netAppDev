using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ObjectFinalizers
{
   // Note: Информируем клиентов о том, что они забыли вызвать Dispose
   public sealed class Win32Heap : IDisposable
   {
      private readonly StackTrace _creationStackTrace;
      private bool _disposed;
      private IntPtr _theHeap;

      public Win32Heap()
      {
         _creationStackTrace = new StackTrace();
         _theHeap = HeapCreate(0, (UIntPtr)0x1000, UIntPtr.Zero);
      }

      [DllImport("kernel32.dll")]
      private static extern IntPtr HeapCreate(uint flOptions, UIntPtr dwInitialSize, UIntPtr dwMaximumSize);

      [DllImport("kernel32.dll")]
      private static extern bool HeapDestroy(IntPtr hHeap);

      #region Реализация IDisposable

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      private void Dispose(bool disposing)
      {
         if (_disposed)
         {
            return;
         }

         if (disposing)
         {
            // Здесь допускается использовать любые внутренние объекты.
            // Этот класс, однако, их не имеет
         }
         else
         {
            // Мы финализировали этот объект, а он не был освобожден.
            // Уведомим пользователя, если только домен приложения не завершит работу.
            var currentDomain = AppDomain.CurrentDomain;
            if (!currentDomain.IsFinalizingForUnload() && !Environment.HasShutdownStarted)
            {
               Console.WriteLine("Сбой в освобождении объекта");
               Console.WriteLine("Объект размещен в:");
               for (var i = 0; i < _creationStackTrace.FrameCount; i++)
               {
                  var frame = _creationStackTrace.GetFrame(i);
                  Console.WriteLine(" {0}", frame);
               }
            }
         }

         // Если используются объекты, о которых известно,
         // что они еще существуют, такие как объекты,
         // реализующие шаблон Singleton, важно убедиться,
         // что они являются безопасными в отношении потоков.
         HeapDestroy(_theHeap);
         _theHeap = IntPtr.Zero;
         _disposed = true;
      }

      ~Win32Heap()
      {
         Dispose(false);
      }

      #endregion
   }
}