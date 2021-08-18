using System.Collections.Generic;
using System.Linq;

namespace Northwind.SqlDataAccess
{
    public class Mapper
    {
        public Domain.Customer Map(Customer customer)
        {
            if (customer == null)
            {
                return null;
            }
            return new Domain.Customer
                        {
                            ID = customer.Customer_ID,
                            City = customer.City,
                            CompanyName = customer.Company_Name,
                            Phone = customer.Phone,
                            PostalCode = customer.Postal_Code
                        };
        }

        public Customer Map(Domain.Customer customer)
        {
            if (customer == null)
            {
                return null;
            }
            return new Customer
            {
                Customer_ID = customer.ID,
                City = customer.City,
                Company_Name = customer.CompanyName,
                Phone = customer.Phone,
                Postal_Code = customer.PostalCode
            };
        }

        public IEnumerable<Domain.Customer> Map(IEnumerable<Customer> customers)
        {
            return customers.Select(Map);
        }
    }
}
