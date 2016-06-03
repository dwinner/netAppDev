using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using Microsoft.ServiceBus;
using Wrox.CarRentalService.Implementations.Europe;
using Wrox.CarRentalService.Contracts;

namespace ServiceBusService
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * The following code is for sample purpose only. Please change the "carrental" and "CarRentalRelayService"
             * with your values. Also change the IssuerName and IssuerSecret with your specific codes.
            */
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Tcp;

            Uri address = ServiceBusEnvironment.CreateServiceUri("sb", "carrental", "CarRentalRelayService");

            NetTcpRelayBinding binding = new NetTcpRelayBinding();
            binding.Security.Mode = EndToEndSecurityMode.None;

            ServiceHost host = new ServiceHost(typeof(CarRentalService));
            ServiceEndpoint endpoint =
                host.AddServiceEndpoint(typeof(ICarRentalService), binding, address);

            TransportClientEndpointBehavior sharedSecretServiceBusCredential = new TransportClientEndpointBehavior();
            sharedSecretServiceBusCredential.CredentialType = TransportClientCredentialType.SharedSecret;
            sharedSecretServiceBusCredential.Credentials.SharedSecret.IssuerName = "owner";
            sharedSecretServiceBusCredential.Credentials.SharedSecret.IssuerSecret = "JJD1in1nCax0EzhfvtKnGZ3YHZN+yi4M85ckTJ+2IFE=";

            endpoint.Behaviors.Add(sharedSecretServiceBusCredential);

            host.Open();

            Console.WriteLine("Waiting for incoming messages...");
            Console.ReadLine();

            host.Close();
        }
    }
}
