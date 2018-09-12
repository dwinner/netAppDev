/**
 * Асинхронный код, основанный на задачах
 */

using System;
using System.Net;
using System.Threading.Tasks;

namespace _04_TaskBasedAsync
{
   class Program
   {
      static void Main()
      {
         LookupHostName();
         Console.WriteLine("Doing other work...");
         Console.ReadKey(true);
      }

      private static void LookupHostName()
      {
         Task<IPAddress[]> ipAddressesPromise = Dns.GetHostAddressesAsync("oreilly.com");
         ipAddressesPromise.ContinueWith(_ =>
            {
               IPAddress[] ipAddresses = ipAddressesPromise.Result;
               foreach (var ipAddress in ipAddresses) // Дальнейшая обработка IP-адресов
               {
                  Console.WriteLine(ipAddress);
               }
            });
      }
   }
}
