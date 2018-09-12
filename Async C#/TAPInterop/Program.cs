/**
 * Взаимодействие с прежними асинхронными шаблонами
 */

using System;
using System.Net;
using System.Threading.Tasks;

namespace _08_TAPInterop
{
   class Program
   {
      private const string HOST_NAME = "google.com";

      static void Main()
      {
         Console.WriteLine("Retrieving ip address list");
         ProcessHostEntry(HOST_NAME);
         Console.WriteLine("Doing other work here");
         Console.ReadKey(true);
      }

      private static async void ProcessHostEntry(string hostNameOrAddress)
      {
         IPHostEntry ipHostEntry = await GetHostEntryAsync(hostNameOrAddress);
         // NOTE: Обработка результатов
         foreach (IPAddress ipAddress in ipHostEntry.AddressList)
         {
            Console.WriteLine("An IP-address of {0} is {1}", ipHostEntry.HostName, ipAddress);
         }
      }

      public static Task<IPHostEntry> GetHostEntryAsync(string hostNameOrAddress)
      {
         TaskCompletionSource<IPHostEntry> taskCompletionSource =
            new TaskCompletionSource<IPHostEntry>();
         Dns.BeginGetHostEntry(hostNameOrAddress, asyncResult =>
            {
               try
               {
                  IPHostEntry ipHostEntry = Dns.EndGetHostEntry(asyncResult);
                  taskCompletionSource.SetResult(ipHostEntry);
               }
               catch (Exception exception)
               {
                  taskCompletionSource.SetException(exception);
               }
            }, null);

         /* NOTE: Или так, что даже эффективнее
         Task<IPHostEntry> task = Task<IPHostEntry>.Factory.FromAsync(Dns.BeginGetHostEntry,
                                                                      Dns.EndGetHostEntry,
                                                                      hostNameOrAddress,
                                                                      null);
          */

         return taskCompletionSource.Task;
      }
   }
}
