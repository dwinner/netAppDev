/**
 * Финализаторы объектов
 */

using System;

namespace ObjectFinalizers
{
   static class FinalizersEntryPoint
   {
      static void Main()
      {
         var win32Heap = new Win32Heap();
         win32Heap = null;
         GC.Collect();
         GC.WaitForPendingFinalizers();

         Console.ReadKey();
      }
   }
}
