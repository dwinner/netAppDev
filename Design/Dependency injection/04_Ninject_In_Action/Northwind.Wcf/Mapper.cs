using System.Collections.Generic;
using System.Linq;
using Northwind.Core;

namespace Northwind.Wcf
{
   public class Mapper : IMapper
   {
      public Customer Map(CustomerContract contract) =>
         new Customer
         {
            Id = contract.Id,
            City = contract.City,
            CompanyName = contract.CompanyName,
            Phone = contract.Phone,
            PostalCode = contract.PostalCode
         };

      public CustomerContract Map(Customer customer) =>
         new CustomerContract
         {
            Id = customer.Id,
            City = customer.City,
            CompanyName = customer.CompanyName,
            Phone = customer.Phone,
            PostalCode = customer.PostalCode
         };

      public IEnumerable<CustomerContract> Map(IEnumerable<Customer> customers) => customers.Select(Map);
   }
}