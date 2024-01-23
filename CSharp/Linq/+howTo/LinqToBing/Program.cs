/**
 * Запросы к web-службе
 */

using System;
using System.Collections.Generic;

namespace LinqToBing
{
   class Program
   {
      //#error Get your own AppId at http://www.bing.com/developers/
      const string APP_ID = "YOUR APPID HERE";

      static void Main()
      {
         Bing search = new Bing(APP_ID);

         const string query = "Visual Studio 2010";

         IEnumerable<SearchResult> results = search.QueryWeb(query);

         foreach (SearchResult result in results)
         {
            Console.WriteLine("{0}"
                + Environment.NewLine + "\t{1}"
                + Environment.NewLine + "\t{2}"
                + Environment.NewLine,
                result.Title, result.Url, result.Description);
         }

         Console.ReadKey();
      }
   }
}
