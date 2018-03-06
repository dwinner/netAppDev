using System;

namespace LanguageBasedAdapter
{
   internal static class Program
   {
      private static void Main()
      {
         // NOTE: Есть метод Add, Значит можно использовать этот синтаксис
         var collection = new CustomCollection {42};
         Console.WriteLine(collection);
      }
   }
}