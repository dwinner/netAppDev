using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Routing;

namespace RouterHost
{
    class Program
    {
        
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("RoutingService");
                ServiceHost serviceHost;
                serviceHost = new ServiceHost(typeof(System.ServiceModel.Routing.RoutingService));
                serviceHost.Open();
                Console.WriteLine("Started");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();

        }
    }
}
