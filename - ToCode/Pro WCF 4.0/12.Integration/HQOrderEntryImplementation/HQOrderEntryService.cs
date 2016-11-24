using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.MsmqIntegration;

using System.Transactions;


using HelperLib;

namespace HQOrderEntryImplementation
{
    public class HQOrderEntryService : HQOrderEntryServiceInterface.IOrderEntryService
    {
        public void SendOrderEntry(MsmqMessage<HQOrderEntryServiceInterface.HQOrderEntry> orderEntryMsg)
        {
            Console.WriteLine("SendOrderEntry");

            try
            {
                if (CheckIfOrderIsValid(orderEntryMsg.Body))
                {
                    Console.WriteLine("Order Is VALID");
                    RouteOrderEntry(ConvertOrderEntrySchema(orderEntryMsg.Body));
                    HQOrderEntryImplementation.SubscriberServiceSingleton.GetInstance().PublishOrderEntrySignal(orderEntryMsg.Body.OrderCustomer.CustomerCountryCode);
                }
                else
                {
                    Console.WriteLine("Order Is not VALID");
                    SendToInvalidOrderQueue(orderEntryMsg);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }


        private bool CheckIfOrderIsValid(HQOrderEntryServiceInterface.HQOrderEntry orderEntry)
        {

            HQProductServiceASMXClient.ServiceSoapClient client;
            client = new HQProductServiceASMXClient.ServiceSoapClient();
            Console.WriteLine("CheckIfOrderIsValid");

            bool orderIsValid;
            orderIsValid = true;

            foreach (var item in orderEntry.OrderOrderedProducts)
            {
                Console.WriteLine(" " + string.Format("checking product {0} for country {1} ", item.ProductID, orderEntry.OrderCustomer.CustomerCountryCode));
                orderIsValid = client.IsProductAvailableForCountry(item.ProductID, orderEntry.OrderCustomer.CustomerCountryCode);
                Console.WriteLine(" " + orderIsValid.ToString());
            }
            return orderIsValid;
        }




        private LocalOrderEntryInterface.LocalOrderEntry ConvertOrderEntrySchema(HQOrderEntryServiceInterface.HQOrderEntry orderEntry)
        {
            Console.WriteLine("ConvertOrderEntrySchema");

            LocalOrderEntryInterface.LocalOrderEntry localOrderEntry;
            localOrderEntry = new LocalOrderEntryInterface.LocalOrderEntry();

            localOrderEntry.CustomerName = orderEntry.OrderCustomer.CustomerName;
            localOrderEntry.CustomerAddressLine1 = orderEntry.OrderCustomer.CustomerAddressLine1;
            localOrderEntry.CustomerAddressLine2 = orderEntry.OrderCustomer.CustomerAddressLine2;
            localOrderEntry.CustomerCountryCode = orderEntry.OrderCustomer.CustomerCountryCode;

            localOrderEntry.OrderOrderedProducts = new List<LocalOrderEntryInterface.OrderedProducts>();

            foreach (var item in orderEntry.OrderOrderedProducts)
            {
                Console.WriteLine(" " + "Translating " + item.ProductName);
                string translation;
                translation = TranslateProductDescription(item.ProductID, orderEntry.OrderCustomer.CustomerCountryCode);
                Console.WriteLine(" " + " into " + translation);

                localOrderEntry.OrderOrderedProducts.Add(new LocalOrderEntryInterface.OrderedProducts { ProductID = orderEntry.OrderCustomer.CustomerCountryCode + "/" + item.ProductID, Quantity = item.Quantity, LocalizedDescription = translation });

            }

            return localOrderEntry;

        }

        private void RouteOrderEntry(LocalOrderEntryInterface.LocalOrderEntry localOrderEntry)
        {
            try
            {
                LocalOrderEntryProxy localOrderEntryProxy;
                localOrderEntryProxy = new LocalOrderEntryProxy();

                int a;
                a = localOrderEntryProxy.SendLocalOrderEntry(localOrderEntry);
                Console.WriteLine("Sent");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }
        }

        private void SendToInvalidOrderQueue(MsmqMessage<HQOrderEntryServiceInterface.HQOrderEntry> orderEntryMsg)
        {
            MsmqIntegrationBinding msmqIntegrationBinding = new MsmqIntegrationBinding();
            msmqIntegrationBinding.Security.Mode = MsmqIntegrationSecurityMode.None;
            //msmqIntegrationBinding.Security.Transport.MsmqAuthenticationMode = MsmqAuthenticationMode.WindowsDomain;
            EndpointAddress endpointAddress = new EndpointAddress("msmq.formatname:DIRECT=OS:.\\private$\\InvalidOrderQueue");
            ChannelFactory<ISendInvalidOrder> channelFactory = new ChannelFactory<ISendInvalidOrder>(msmqIntegrationBinding, endpointAddress);
            ISendInvalidOrder channel = channelFactory.CreateChannel();
            orderEntryMsg.TimeToReachQueue = new TimeSpan(0, 1, 0);
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                channel.SendInvalidOrder(orderEntryMsg);
                scope.Complete();
            }

            Console.WriteLine("Order Sent to InvalidOrderQueue");


        }

        private string TranslateProductDescription(string productID, string languageCode)
        {

            System.Net.HttpWebRequest webrequest;
            webrequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(
              string.Format(@"http://localhost:8082/HQLocalizationService/TranslateProductDescriptions/{0}/{1}", languageCode, productID));

            webrequest.ContentLength = 0;

            System.Net.HttpWebResponse webresponse;

            webresponse = (System.Net.HttpWebResponse)webrequest.GetResponse();

            Encoding enc = System.Text.Encoding.GetEncoding(1252);
            System.IO.StreamReader loResponseStream = new System.IO.StreamReader(webresponse.GetResponseStream(), enc);

            string response = loResponseStream.ReadToEnd();

            string answer;
            answer = GenericSerializer<string>.DeserializeDC(response);

            return answer;

        }
    }
}
