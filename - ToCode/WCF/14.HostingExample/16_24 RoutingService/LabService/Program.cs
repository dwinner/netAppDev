using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri[] baseAddress = new Uri[] { new Uri("net.tcp://localhost:9051/labservice") };
            ServiceHost host = new ServiceHost(typeof(LabService), baseAddress);

            host.AddDefaultEndpoints();

            try
            {
                host.Open();

                foreach (var endpoint in host.Description.Endpoints)
                {
                    Console.WriteLine("Listen on " + endpoint.Address.Uri.ToString());
                }

                Console.WriteLine("LabService host opened. Wait for incoming request...");
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
