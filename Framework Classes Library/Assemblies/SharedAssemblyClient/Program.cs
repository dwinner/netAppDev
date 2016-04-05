/**
 * Клиент, использующий сборку из GAC
 */

using System;

namespace SharedAssemblyClient
{
   class Program
   {
      static void Main()
      {
         var quotes = new SharedDemo.SharedDemo("Quotes.txt");
         Console.WriteLine(quotes.FullName);
         for (int i = 0; i < 3; i++)
         {
            Console.WriteLine(quotes.GetQuoteOfTheDay());
            Console.WriteLine();
         }
      }
   }
}
