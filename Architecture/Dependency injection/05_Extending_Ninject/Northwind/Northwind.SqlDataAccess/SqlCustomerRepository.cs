using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.Domain;

namespace Northwind.SqlDataAccess
{
    public class SqlCustomerRepository : ICustomerRepository
    {
        private readonly Mapper mapper;
        private readonly NorthwindEntities context;

        public SqlCustomerRepository(string connectionString, Mapper mapper)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            this.mapper = mapper;
            context = new NorthwindEntities(connectionString);
        }

        public IEnumerable<Domain.Customer> GetAll()
        {
            return mapper.Map(context.Customers);
        }

        public Domain.Customer Get(string customerID)
        {
            var customer = context.Customers
                .SingleOrDefault(c => c.Customer_ID == customerID);

            return mapper.Map(customer);
        }

        public void Add(Domain.Customer domainCustomer)
        {
            var customer = mapper.Map(domainCustomer);
            context.Customers.AddObject(customer);
            context.SaveChanges();
        }
    }
}
