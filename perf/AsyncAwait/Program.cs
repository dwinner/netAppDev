using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AsyncAwait
{
   internal static class Program
   {
      // You can do other work here while the async task is running
      private static readonly Regex Regex = new Regex("<title>(.*)</title>", RegexOptions.Compiled);

      private static void Main()
      {
         GetWebPageTitle(@"Http://www.bing.com").ContinueWith(task => Console.WriteLine(task.Result));

         Console.WriteLine("Press any key to exit...");
         Console.ReadKey();
      }

      private static async Task<string> GetWebPageTitle(string url)
      {
         var client = new HttpClient();
         var task = client.GetStringAsync(url);

         // now we need the result so await
         var contents = await task;

         var match = Regex.Match(contents);
         if (match.Success)
         {
            return match.Groups[1].Captures[0].Value;
         }

         return string.Empty;
      }
   }
}