using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthcareServiceLibrary;
using System.ServiceModel;

namespace RoutingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ILabService> labFactory = new ChannelFactory<ILabService>(new BasicHttpBinding(), "http://localhost:9001/routing");
            ChannelFactory<IHospitalService> hospitalFactory = new ChannelFactory<IHospitalService>(new BasicHttpBinding(), "http://localhost:9001/routing");

            try
            {
                labFactory.Open();
                hospitalFactory.Open();

                ILabService labClient = labFactory.CreateChannel();
                IHospitalService hospitalClient = hospitalFactory.CreateChannel();

                string input;
                do
                {
                    Console.Write("Press to send lab request");
                    input = Console.ReadLine();
                    labClient.Save(new SomeOtherData());                    
                    if (input == "exit") break;

                    Console.Write("Press to send hospital request");
                    input = Console.ReadLine();
                    hospitalClient.Save(new SomeData());                    
                    if (input == "exit") break;
                    
                } while (true);


                Console.ReadLine();
            }
            catch (Exception)
            {
                labFactory.Abort();
                hospitalFactory.Abort();
                throw;
            }
            finally
            {
                labFactory.Close();
                hospitalFactory.Close();
            }

        }
    }
}
