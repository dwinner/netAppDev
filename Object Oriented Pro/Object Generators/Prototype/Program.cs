/**
 * Прототипы объекта
 */

using System;

namespace Prototype
{
   static class Program
   {
      static void Main()
      {
         var address = new Address();

         Address deepAddress = Copy(address);
         deepAddress.Street = "Revolution street";

         Address shallowAddress = Copy(deepAddress, false);
         shallowAddress.Street = "Redwood street";
         
         Console.WriteLine(shallowAddress);
         Console.WriteLine(deepAddress);

         Console.ReadKey();
      }

      private static T Copy<T>(ICopy<T> objectToCopy, bool deep = true)
      {
         return objectToCopy.Copy(deep);
      }
   }
}
