/**
 * Одноразовые объекты
 */

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DisposableObjects
{
   sealed class DisposableEntryPoint
   {
      static void Main()
      {
         var streamWriter = new StreamWriter("Output.txt");
         try
         {
            streamWriter.WriteLine("Проверка аварийного механизма освобождения");
         }
         finally
         {
            streamWriter.Dispose();
         }

         using (streamWriter = new StreamWriter("Output2.txt"))
         {
            streamWriter.WriteLine("Проверка аварийного механизма освобождения");
         }

         Console.ReadKey();
      }
   }

   // Note: Этот класс не нуждается в том, чтобы проходить по дереву включения, вызывая Dispose
   public sealed class Win32Heap : IDisposable
   {
      private IntPtr _heap;
      private bool _disposed;

      [DllImport("kernel32.dll")]
      private static extern IntPtr HeapCreate(uint flOptions, UIntPtr dwInitialSize, UIntPtr dwMaximumSize);

      [DllImport("kernel32.dll")]
      private static extern bool HeapDestroy(IntPtr hHeap);

      public Win32Heap()
      {
         _heap = HeapCreate(0, (UIntPtr)0x1000, UIntPtr.Zero);
      }

      public void Dispose()
      {
         if (!_disposed)
         {
            HeapDestroy(_heap);
            _heap = IntPtr.Zero;
            _disposed = true;
         }
      }
   }
}
