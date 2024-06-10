/**
 * Финализаторы объектов
 */

using System;

namespace ObjectFinalizers
{
   internal static class FinalizersEntryPoint
   {
      private static void Main()
      {
         var win32Heap = new Win32Heap();
         win32Heap = null;
         GC.Collect();
         GC.WaitForPendingFinalizers();

         Console.ReadKey();
      }
   }
}