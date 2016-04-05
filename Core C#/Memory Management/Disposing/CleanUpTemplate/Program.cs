using System;

namespace CleanUpTemplate
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Dispose/Destructor combinator");

         // 1) Явный контекст освобождения объектов

         ResourceWrapper rw1 = null;
         ResourceWrapper rw2 = new ResourceWrapper(); // Забыли про освобождение
         try
         {
            rw1 = new ResourceWrapper();
         }
         finally
         {
            if (rw1 != null)
               rw1.Dispose();
         }

         // 2) Неявный контекст освобождения объектов

         using (ResourceWrapper rw3 = new ResourceWrapper())
         using (ResourceWrapper rw4 = new ResourceWrapper())
         {
            // Что-то делать с объектами
         }

         Console.ReadKey();
      }
   }
}
