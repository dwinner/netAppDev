using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrox.CarRentalService.Implementations.Europe;
using System.ServiceModel;
using System.ServiceModel.Description;
using Wrox.CarRentalService.Contracts;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost carRentalHost = new ServiceHost(typeof(CarRentalService));

            try
            {
                carRentalHost.Open();

                Console.WriteLine("The car rental service is up.");                
                Console.ReadLine();

                carRentalHost.Close();
            }
            catch (CommunicationException ex)
            {
                carRentalHost.Abort();
                Console.WriteLine(ex);
            }
            catch (TimeoutException ex)
            {
                carRentalHost.Abort();
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                carRentalHost.Abort();
                Console.WriteLine(ex);
            }
        }
    }
}
