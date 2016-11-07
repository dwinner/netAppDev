using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using Microsoft.ServiceBus;
using Wrox.CarRentalService.Contracts;

namespace ServiceBusClient
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * The following code is for sample purpose only. Please change the "carrental" and "CarRentalRelayService"
             * with your values. Also change the IssuerName and IssuerSecret with your specific codes.
            */
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;

            Uri address = ServiceBusEnvironment.CreateServiceUri("sb", "carrental", "CarRentalRelayService");

            NetEventRelayBinding binding = new NetEventRelayBinding();
            binding.Security.Mode = EndToEndSecurityMode.None;

            TransportClientEndpointBehavior sharedSecretServiceBusCredential = new TransportClientEndpointBehavior();
            sharedSecretServiceBusCredential.CredentialType = TransportClientCredentialType.SharedSecret;
            sharedSecretServiceBusCredential.Credentials.SharedSecret.IssuerName = "owner";
            sharedSecretServiceBusCredential.Credentials.SharedSecret.IssuerSecret = "JJD1in1nCax0EzhfvtKnGZ3YHZN+yi4M85ckTJ+2IFE=";

            ServiceEndpoint endpoint = new ServiceEndpoint(
                    ContractDescription.GetContract(typeof (ICarRentalService)),
                    binding, 
                    new EndpointAddress(address));
            endpoint.Behaviors.Add(sharedSecretServiceBusCredential);

            ChannelFactory<ICarRentalService> channelFactory =
                new ChannelFactory<ICarRentalService>(endpoint);
            ICarRentalService ps = channelFactory.CreateChannel();

            string message;
            do
            {
                Console.Write("Inserisci un messaggio: ");
                message = Console.ReadLine();
                if (!string.IsNullOrEmpty(message))
                {
                    ps.CalculatePrice(DateTime.Now, DateTime.Now.Add(TimeSpan.FromHours(8)), message, "");
                    Console.WriteLine("Messaggio inviato");
                }
            } while (!string.IsNullOrEmpty(message));

            Console.ReadLine();
        }
    }
}
