/**
 * Хост для WCF-службы.
 */

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using MagicEightBallServiceLib;

namespace MagicEightBallServiceHost
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("----- Console based WCF Host -----");
         using (ServiceHost serviceHost = new ServiceHost(typeof(MagicEightBallService)))
         {
            serviceHost.Open();
            DisplayHostInfo(serviceHost);
            Console.WriteLine("The service is ready");
            Console.WriteLine("Press the Enter key to terminate");
            Console.ReadLine();
         }         
      }

      static void DisplayHostInfo(ServiceHost aServiceHost) // Информация о текущем хост-процессе
      {
         Console.WriteLine();
         Console.WriteLine("----- Host info -----");
         foreach (ServiceEndpoint endpoint in aServiceHost.Description.Endpoints)
         {
            Console.WriteLine("Address: {0}", endpoint.Address);
            Console.WriteLine("Binding: {0}", endpoint.Binding.Name);
            Console.WriteLine("Contract: {0}{1}", endpoint.Name, Environment.NewLine);
         }
         Console.WriteLine();
      }
   }
}
