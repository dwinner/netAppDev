/**
 * Детерминированная очистка совместно с финализаторами
 */

using System;

namespace Finalizer
{
   class Program
   {
      static void Main(string[] args)
      {
         using (WrappedResource res = new WrappedResource("TestFile.txt"))
         {
            Console.WriteLine("Using resource...");
         }

         WrappedResource wrappedRes = new WrappedResource("TestFile.txt"); // Здесь отработает финализатор

         Console.ReadKey();
      }
   }
}
