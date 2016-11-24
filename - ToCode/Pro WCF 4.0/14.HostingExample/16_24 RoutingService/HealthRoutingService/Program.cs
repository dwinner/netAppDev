using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Routing;
using System.ServiceModel;

namespace HealthRoutingService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(RoutingService));

            try
            {
                host.Open();

                foreach (var endpoint in host.Description.Endpoints)
                {
                    Console.WriteLine("Listen on " + endpoint.Address.Uri.ToString());
                }

                Console.WriteLine("Host opened. Wait for incoming request...");
                Console.Read();
            }
            catch (Exception)
            {
                host.Abort();
                throw;
            }
            finally
            {
                host.Close();
            }
        }
    }
}
