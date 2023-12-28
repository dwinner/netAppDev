using System;

namespace AnonymousTypes
{
   internal static class Program
   {
      private static void Main()
      {
         var part = new { ID = 1, Name = "Part01", Weight = 2.5 };

         Console.WriteLine("var Part, Weight: {0}", part.Weight);
         Console.WriteLine("var Part, ToString(): {0}", part);
         Console.WriteLine("var Part, Type: {0}", part.GetType());
         Console.ReadKey();
      }
   }
}