/**
 * Что делает async
 */

using System;
using System.Net;

namespace _01_StartUp
{
   class Program
   {
      private const string Uri1 = "http://www.gotdotnet.ru/";
      private const string Uri2 = "http://mirknig.com/index.php";

      static void Main()
      {
         DumpWebPage(Uri1);   // блокирующий вызов
         Console.WriteLine("Doing other work...");
         Console.ReadKey(true);

         DumpWebPageAsync(Uri2); // неблокирующий вызов
         Console.WriteLine("Doing another work...");

         Console.Write("Press any key to continue...");
         Console.ReadKey();
      }

      private static void DumpWebPage(string uri)  // Блокирующий метод загрузки web-страницы
      {
         using (WebClient webClient = new WebClient())
         {
            string page = webClient.DownloadString(uri);
            Console.WriteLine(page);
         }
      }

      private static async void DumpWebPageAsync(string uri) // Асинхронный метод загрузки web-страницы
      {
         using (WebClient webClient = new WebClient())
         {
            string page = await webClient.DownloadStringTaskAsync(uri); // note: возвращаем управление вызывающему методу
            Console.WriteLine(page);
         }
      }
   }
}
