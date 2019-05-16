using System;
using System.Collections.Generic;
using System.Linq;
using Northwind.Core;

namespace Northwind.SqlDataAccess
{
   public class SqlCustomerRepository : ICustomerRepository
   {
      private readonly NorthwindEntities _context;
      private readonly Mapper _mapper;

      public SqlCustomerRepository(Mapper mapper, NorthwindEntities context)
      {
         _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
         _context = context ?? throw new ArgumentNullException(nameof(context));
      }

      IEnumerable<Core.Customer> ICustomerRepository.GetAll() => _mapper.Map(_context.Customers);

      public Core.Customer Get(string customerId)
      {
         var customer = _context.Customers.SingleOrDefault(c => c.Customer_ID == customerId);
         return _mapper.Map(customer);
      }

      public void Add(Core.Customer domainCustomer)
      {
         var customer = _mapper.Map(domainCustomer);
         _context.Customers.AddObject(customer);
         _context.SaveChanges();
      }
   }
}