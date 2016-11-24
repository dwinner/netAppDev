using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;
using CarManagementService;
using RentalService;
using CustomerService;
using ExternalInterfaceFacade;

namespace HostAllServices
{
  class Program
  {
    static ServiceHost CarManagementServiceHost;
    static ServiceHost CustomerServiceHost;
    static ServiceHost RentalServiceHost;
    static ServiceHost ExternalServiceHost;

    static void Main(string[] args)
    {
      Console.WriteLine("ServiceHost");
      try
      {
        CarManagementServiceHost = new ServiceHost(typeof(CarManagementService.CarManagementImplementation));
        CarManagementServiceHost.Open();

        CustomerServiceHost = new ServiceHost(typeof(CustomerService.CustomerServiceImplementation));
        CustomerServiceHost.Open();

        RentalServiceHost = new ServiceHost(typeof(RentalService.RentalServiceImplementation));
        RentalServiceHost.Open();

        ExternalServiceHost = new ServiceHost(typeof(ExternalInterfaceFacade.ExternalInterfaceFacadeImplementation));
        ExternalServiceHost.Open();


      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      Console.WriteLine("Started");
      Console.ReadKey();
    }
  }
}
