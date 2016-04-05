/**
 * Простая проверка функциональности сервера
 */

using System;

namespace TestQuoteServer
{
   class Program
   {
      private const string QuotesFileName = "Quotes.txt";
      private const int Port = 4567;

      static void Main()
      {
         var quoteServer = new QuoteServer.QuoteServer(QuotesFileName, Port);
         quoteServer.Start();
         Console.WriteLine("Hit return to exit");
         Console.ReadLine();
         quoteServer.Stop();
      }
   }
}
