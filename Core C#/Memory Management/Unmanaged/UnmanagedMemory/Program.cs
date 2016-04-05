/**
 * Выделение неуправляемой памяти
 */

using System;

namespace UnmanagedMemory
{
   class Program
   {
      static void Main()
      {
         Console.WriteLine("Memory usage before unmanaged allocation: {0:N0}",
            GC.GetTotalMemory(false));
         var obj = new MyDataClass(10000000);
         Console.WriteLine("Unmanaged address is {0}", obj.Memory);

         // Неуправляемая память не учтена!
         Console.WriteLine("Memory usage after unmanaged allocation: {0:N0}",
            GC.GetTotalMemory(false));

         GC.AddMemoryPressure(obj.MemorySize);

         Console.ReadKey();
      }
   }
}
