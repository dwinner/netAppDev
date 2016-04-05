/**
 * Асинхронные методы "заразны"
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace _06_AsyncDistribution
{
   internal static class Program
   {
      private static readonly IList<string> Urls = new List<string>
      {
         "http://www.google.com",
         "http://www.rotapost.ru",
         "http://mail.ru",
         "https://www.facebook.com",
         "http://www.oracle.com",
         "http://www.microsoft.com",
         "http://rutracker.org",
         "http://tula.job.ru",
         "http://voronezh.hh.ru",
         "http://rookee.ru"
      };

      private static void Main()
      {
         PrintResults();
         Console.WriteLine("Doing other work...");
         Console.ReadKey(true);
      }

      private static async void PrintResults()
      {
         try
         {
            var resultTuple = await FindLargestWebPageAsync(Urls);
            // NOTE: Получить результат
            Console.WriteLine("Url: {0} Size: {1}", resultTuple.Item1, resultTuple.Item2);
         }
         catch (WebException webEx)
         {
            Console.WriteLine(webEx);
         }
      }

      /// <summary>
      ///    Получение размера страницы по заданному URL
      /// </summary>
      /// <remarks>Этот метод не блокирует вызывающий поток</remarks>
      /// <param name="url">URL страницы</param>
      /// <returns>Размер страницы</returns>
      private static async Task<int> GetPageSizeAsync(string url)
      {
         using (var webClient = new WebClient())
         {
            var page = await webClient.DownloadStringTaskAsync(url);
            return page.Length;
         }
      }

      /// <summary>
      ///    Нахождение самой большой по размеру страницы
      /// </summary>
      /// <remarks>Этот метод не блокирует вызывающий поток</remarks>
      /// <param name="urls">Набор страниц</param>
      /// <returns>Кортеж [URL-страницы, Размер страницы]</returns>
      private static async Task<Tuple<string, int>> FindLargestWebPageAsync(IEnumerable<string> urls)
      {
         string largestContent = null;
         var largestSize = 0;
         foreach (var url in urls)
         {
            var size = await GetPageSizeAsync(url);
            if (size > largestSize)
            {
               largestSize = size;
               largestContent = url;
            }
         }
         return Tuple.Create(largestContent, largestSize);
      }
   }
}