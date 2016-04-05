/**
 * Корректная обработка ошибок в асинхронных методах
 */

using System;
using System.Net;
using System.Threading.Tasks;

namespace FailFastCorrect
{
   class Program
   {
      static void Main()
      {
         WebDownloader mainGooglePage = new WebDownloader("http://google.com");
         ProcessDownload(mainGooglePage);
         mainGooglePage = new WebDownloader("http://abvgde.tutu");
         ProcessDownload(mainGooglePage);

         Console.ReadKey();
      }

      private static async void ProcessDownload(WebDownloader webDownloader)
      {
         string downloadedContent = await webDownloader.RetrieveContentAsync();
         // NOTE: Дальнейшая обработка результата, полученного асинхронно
         if (webDownloader.LastError != null)
         {
            Console.WriteLine("Error: {0}", webDownloader.LastError.Message);
         }
         else
         {
            Console.WriteLine("Content from {0}", webDownloader.Url);
         }
      }
   }

   class WebDownloader
   {
      public Exception LastError { get; private set; }

      public string Url { get; set; }

      internal WebDownloader(string aUrl)
      {
         Url = aUrl;
      }

      internal async Task<string> RetrieveContentAsync()
      {
         string page = null;
         try
         {
            using (WebClient webClient = new WebClient())
            {
               page = await webClient.DownloadStringTaskAsync(Url);
            }
            LastError = null;
         }
         catch (WebException webEx)
         {
            LastError = webEx;
         }
         return LastError != null ? string.Empty : page;
      }
   }
}
