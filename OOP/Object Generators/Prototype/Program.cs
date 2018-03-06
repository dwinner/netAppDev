/**
 * Прототипы объекта
 */

using System;

namespace Prototype
{
   internal static class Program
   {
      private static void Main()
      {
         var address = new Address();
         var deepAddress = Copy(address);
         var shallowAddress = Copy(deepAddress, false);

         Console.WriteLine(shallowAddress);
         Console.WriteLine(shallowAddress.City);
         Console.WriteLine(shallowAddress.State);
         Console.WriteLine(shallowAddress.Street);
         Console.WriteLine(shallowAddress.ZipCode);
         Console.WriteLine(shallowAddress.Type);
         Console.WriteLine(deepAddress);

         UsingCopeExt();
         UsingUniversalCopy();
         UsingICopy();

         Console.ReadKey();
      }

      private static void UsingCopeExt()
      {
         var address = new Address("Local", "Revolution", "Tula", "TulaSt", "301360");
         var deepCopy = (Address) address.DeepCopy();
         Console.WriteLine(deepCopy);
      }

      private static void UsingUniversalCopy()
      {
         var address = new Address("Local", "Revolution", "Tula", "TulaSt", "301360");
         var deepCopy = UniversalCopyUtility<Address>.DeepCopy(address);
         Console.WriteLine(deepCopy);
      }

      private static void UsingICopy()
      {
         ICopy<Address> address = new Address("Local", "Revolution", "Tula", "TulaSt", "301360");
         var copy = address.Copy();
         Console.WriteLine(copy);
      }      

      private static T Copy<T>(ICopy<T> objectToCopy, bool deep = true)
         => objectToCopy.Copy(deep);

      private static T Copy<T>(T objToCopy)
         => UniversalCopyUtility<T>.DeepCopy(objToCopy);
   }
}