/**
 * Множественные вызовы асинхронных методов
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace _05_ManyAsync
{
   class Program
   {
      private static readonly IList<string> HostNames = new List<string>
         {
            "google.com",
            "oracle.com",
            "stackoverflow.com",
            "gotdotnet.ru",
            "microsoft.com"
         };

      static void Main()
      {
         LookupHostNames(HostNames);
         Console.WriteLine("Doing other work here...");
         Console.ReadKey();
      }

      private static void LookupHostNames(IList<string> hostNames)
      {
         if (hostNames != null && hostNames.Count > 0)
         {
            LookUpHostNamesHelper(hostNames, 0);
         }
      }

      private static void LookUpHostNamesHelper(IList<string> hostNames, int i)
      {
         Task<IPAddress[]> ipAddressesPromise = Dns.GetHostAddressesAsync(hostNames[i]);
         ipAddressesPromise.ContinueWith(_ =>
            {
               IPAddress[] ipAddresses = ipAddressesPromise.Result;
               foreach (IPAddress ipAddress in ipAddresses) // NOTE: Обработать адреса
               {
                  Console.WriteLine(ipAddress);
               }
               if (i + 1 < hostNames.Count)
               {
                  LookUpHostNamesHelper(hostNames, i + 1);
               }
            });
      }
   }
}
