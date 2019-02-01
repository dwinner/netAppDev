using System;

namespace PsIterating
{
   internal class Program
   {
      private static void Main()
      {
         var customerRepository = new CustomerRepository();
         var names = customerRepository.GetAllNames();

         foreach (var name in names)
         {
            Console.WriteLine("Processed: {0}", name);
         }

         Console.ReadKey();
      }
   }
}