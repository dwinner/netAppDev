using System;

namespace Singleton
{
   static class Program
   {
      static void Main()
      {
         Address singleAddress = GenTsSingleton<Address>.Instance;
         Address anotherAddress = GenTsSingleton<Address>.Instance;

         Console.WriteLine(singleAddress.GetHashCode());
         Console.WriteLine(anotherAddress.GetHashCode());
         Console.WriteLine(singleAddress == anotherAddress ? "Ok" : "Not ok");

         GC.Collect();

         Address addressAfterGc = GenTsSingleton<Address>.Instance;
         Console.WriteLine(addressAfterGc.GetHashCode());

         Console.ReadKey();
      }
   }
}
