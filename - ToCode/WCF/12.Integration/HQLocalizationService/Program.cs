using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel.Web;

namespace HQLocalizationService
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("TranslateProductDescriptions");
            WebServiceHost webServiceHost;
            webServiceHost = new WebServiceHost(typeof(TranslateProductDescriptions));
            webServiceHost.Open();

            Console.ReadKey();
        }
    }
}
