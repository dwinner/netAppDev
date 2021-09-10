/**
 * Освобождение неуправляемых ресурсов с помощью финализации
 */

using System;

namespace Dispose
{
   class Program
   {
      static void Main()
      {
         WrappedResource wrappedRes = new WrappedResource("TestFile.txt");
         // wrappedRes.Close(); Note: Если вдруг мы забыли это сделать
         Console.ReadKey();
      }
   }
}
