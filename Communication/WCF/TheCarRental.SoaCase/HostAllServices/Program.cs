/**
 * Хостинг всех размещаемых служб
 */

using System;
using System.ServiceModel;
using CarManagementService;
using CustomerService;
using ExternalInterfaceFacade;
using RentalService;

namespace HostAllServices
{
   internal static class Program
   {
      private static ServiceHost _carManagementServiceHost;
      private static ServiceHost _customerServiceHost;
      private static ServiceHost _rentalServiceHost;
      private static ServiceHost _externalServiceHost;

      private static void Main()
      {
         Console.WriteLine("ServiceHost");
         try
         {
            // Открываем размещающее приложение
            _carManagementServiceHost = new ServiceHost(typeof(CarManagementImpl));
            _carManagementServiceHost.Open();

            _customerServiceHost = new ServiceHost(typeof(CustomerServiceImpl));
            _customerServiceHost.Open();

            _rentalServiceHost = new ServiceHost(typeof(RentalServiceImpl));
            _rentalServiceHost.Open();

            _externalServiceHost = new ServiceHost(typeof(ExternalInterfacefacadeImpl));
            _externalServiceHost.Open();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
      }
   }
}