using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace ReceiveApprovedHolidayRequestsService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("HOST : ReceiveApprovedHolidayRequestsService");
                ServiceHost serviceHost;
                serviceHost = new ServiceHost(typeof(ReceiveApprovedHolidayRequestService));
                serviceHost.Open();
                Console.WriteLine("started");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
