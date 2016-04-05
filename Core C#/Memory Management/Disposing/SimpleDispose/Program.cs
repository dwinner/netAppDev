using System;

namespace SimpleDispose
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with Dispose *****\n");

         // Метод Dispose() вызывается автоматически при выходе за пределы using.
         using (MyResourceWrapper rw = new MyResourceWrapper(),
               rw2 = new MyResourceWrapper())
         {
            // Use rw and rw2 objects.
         }

         Console.ReadKey();
      }

   }

}
