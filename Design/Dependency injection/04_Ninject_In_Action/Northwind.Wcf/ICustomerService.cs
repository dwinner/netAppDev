using System.Collections.Generic;
using System.ServiceModel;

namespace Northwind.Wcf
{
   [ServiceContract]
   public interface ICustomerService
   {
      [OperationContract]
      IEnumerable<CustomerContract> GetAllCustomers();

      [OperationContract]
      void AddCustomer(CustomerContract customer);
   }
}