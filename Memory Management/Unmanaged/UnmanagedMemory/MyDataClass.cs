using System;
using System.Runtime.InteropServices;

namespace UnmanagedMemory
{
   unsafe class MyDataClass
   {
      private readonly IntPtr _memory = IntPtr.Zero;

      public IntPtr Memory
      {
         get { return _memory; }
      }

      public int NumObjects { get; private set; }
      public int MemorySize { get { return sizeof(int) * NumObjects; } }

      public MyDataClass(int numObjects)
      {
         NumObjects = numObjects;

         _memory = Marshal.AllocHGlobal(MemorySize);

         // Мы должны сообщить сборщику мусора, что используем дополнительный
         // объем памяти, чтобы он смог лучше спланировать свою работу
         // Note: Значение, возвращаемое методом GC.GetTotalMemory, остается прежним

         GC.AddMemoryPressure(MemorySize);

         Int32* pI = (Int32*)_memory;
         for (int i = 0; i < NumObjects; ++i)
         {
            pI[i] = i;
         }
      }

      // Для неуправляемых ресурсов требуется финализатор, чтобы
      // у вас была уверенность, что они освобождены
      ~MyDataClass()
      {
         if (_memory != IntPtr.Zero)
         {
            Marshal.FreeHGlobal(_memory);
            // Сообщить сборщику мусора о дефиците памяти
            GC.RemoveMemoryPressure(MemorySize);
         }
      }
   }
}
