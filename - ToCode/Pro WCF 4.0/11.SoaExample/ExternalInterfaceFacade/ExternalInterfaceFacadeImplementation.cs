using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.ServiceModel;

using System.Transactions;

using ExternalInterface;

namespace ExternalInterfaceFacade
{
    public class ExternalInterfaceFacadeImplementation : IExternalInterface
    {
        public void SubmitRentalContract(RentalContract rentalContract)
        {
            Console.WriteLine("SubmitRentalContract");
            Console.WriteLine(rentalContract.Company);
            Console.WriteLine(rentalContract.CompanyReferenceID);
            Console.WriteLine(rentalContract.RentalRegistration.PickUpLocation);

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {

                try
                {
                    NetNamedPipeBinding netNamedPipeBinding;
                    netNamedPipeBinding = new NetNamedPipeBinding();
                    netNamedPipeBinding.TransactionFlow = true;


                    CustomerInterface.ICustomer customerServiceChannel;
                    customerServiceChannel = ChannelFactory<CustomerInterface.ICustomer>.CreateChannel(netNamedPipeBinding, new EndpointAddress("net.pipe://localhost/customerservice"));

                    int newCustomerID;
                    newCustomerID = customerServiceChannel.RegisterCustomer(rentalContract.Customer);
                    Console.WriteLine("CustomerRegistered");
                    rentalContract.RentalRegistration.CustomerID = newCustomerID;

                    //throw new DivideByZeroException();

                    RentalInterface.IRental rentalServiceChannel;
                    rentalServiceChannel = ChannelFactory<RentalInterface.IRental>.CreateChannel(netNamedPipeBinding, new EndpointAddress("net.pipe://localhost/rentalservice"));

                    rentalServiceChannel.RegisterCarRental(rentalContract.RentalRegistration);
                    Console.WriteLine("CarRentalRegistered");
                    scope.Complete();
                    Console.WriteLine("-- Scope Complete --");
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}
