using System;

namespace Singleton
{
   internal static class Program
   {
      private static void Main()
      {
         var singleAddress = GenTsSingleton<Address>.Instance;
         var anotherAddress = GenTsSingleton<Address>.Instance;

         Console.WriteLine(singleAddress.GetHashCode());
         Console.WriteLine(anotherAddress.GetHashCode());
         Console.WriteLine(singleAddress == anotherAddress ? "Ok" : "Not ok");

         GC.Collect();

         var addressAfterGc = GenTsSingleton<Address>.Instance;
         Console.WriteLine(addressAfterGc.GetHashCode());

         Console.ReadKey();
      }
   }
}