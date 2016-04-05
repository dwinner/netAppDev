/**
 * Простой пример использования типа dynamic
 */

using System;

namespace DynamicSample
{
   static class Program
   {
      static void Main()
      {
         dynamic dyn = 100;
         Console.WriteLine(dyn.GetType());
         Console.WriteLine(dyn);

         dyn = "This is a string";
         Console.WriteLine(dyn.GetType());
         Console.WriteLine(dyn);

         dyn = new Person { FirstName = "Bugs", LastName = "Bunny" };
         Console.WriteLine(dyn.GetType());
         Console.WriteLine("{0} {1}", dyn.FirstName, dyn.LastName);
      }
   }
}
