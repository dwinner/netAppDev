using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.ServiceModel;

namespace HQOrderEntryServiceHost
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            try
            {
                ServiceHost serviceHostOrderEntryService;
                serviceHostOrderEntryService = new ServiceHost(typeof(HQOrderEntryImplementation.HQOrderEntryService));
                serviceHostOrderEntryService.Open();

                ServiceHost serviceHostSubscribeService;
                HQOrderEntryImplementation.SubscribeService subscribeService;
                subscribeService = HQOrderEntryImplementation.SubscriberServiceSingleton.GetInstance();
                serviceHostSubscribeService = new ServiceHost(subscribeService);
                serviceHostSubscribeService.Open();

                //foreach (var e in serviceHostOrderEntryService.Description.Endpoints)
                //{
                //    Console.WriteLine(string.Format(" {0} on {1}", e.Binding.GetType().Name.ToString(), e.Address.Uri.ToString()));
                //}
                //foreach (var e in serviceHostSubscribeService.Description.Endpoints)
                //{
                //    Console.WriteLine(string.Format(" {0} on {1}", e.Binding.GetType().Name.ToString(), e.Address.Uri.ToString()));
                //}

                Console.WriteLine("OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
            Console.ReadKey();
        }
    }
}
