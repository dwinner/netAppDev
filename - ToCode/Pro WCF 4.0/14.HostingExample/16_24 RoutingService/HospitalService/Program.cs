using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using HealthcareServiceLibrary;

namespace Hospital
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri[] baseAddress = new Uri[] { new Uri("net.tcp://localhost:9050/hospitalservice") };
            ServiceHost host = new ServiceHost(typeof(HospitalService), baseAddress);

            host.AddDefaultEndpoints();

            try
            {
                host.Open();

                foreach (var endpoint in host.Description.Endpoints)
                {
                    Console.WriteLine("Listen on " + endpoint.Address.Uri.ToString() + " for contract " + endpoint.Contract.Name);
                }

                Console.WriteLine("HospitalService host opened. Wait for incoming request...");
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
