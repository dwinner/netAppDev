/**
 * Использование интерфейса IAsyncResult
 */

using System;
using System.Net;

namespace _03_AsyncResultBased
{
   class Program
   {
      static void Main()
      {
         LookupHostName();
         Console.WriteLine("Doing other work here...");
         Console.ReadKey();
      }

      private static void LookupHostName()
      {
         object unrelatedObject = "hello";
         Dns.BeginGetHostAddresses("oreilly.com", OnHostNameResolved, unrelatedObject);
      }

      private static void OnHostNameResolved(IAsyncResult ar)
      {
         object unrelatedObject = ar.AsyncState;
         IPAddress[] addresses = Dns.EndGetHostAddresses(ar);
         Console.WriteLine("Unrelated object: {0}", unrelatedObject);
         foreach (var ipAddress in addresses)
         {
            Console.WriteLine(ipAddress);
         }
      }
   }
}
