using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace ArgentinaHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Argentina host");
                ServiceHost serviceHost;
                serviceHost = new ServiceHost(typeof(ArgentinaOrderEntryImplemenation));
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
