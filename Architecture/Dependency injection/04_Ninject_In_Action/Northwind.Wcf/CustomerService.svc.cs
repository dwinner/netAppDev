using System;
using System.Collections.Generic;
using Northwind.Core;

namespace Northwind.Wcf
{
   public class CustomerService : ICustomerService
   {
      private readonly IMapper _mapper;
      private readonly ICustomerRepository _repository;

      public CustomerService(ICustomerRepository repository, IMapper mapper)
      {
         _repository = repository ?? throw new ArgumentNullException(nameof(repository));
         _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public IEnumerable<CustomerContract> GetAllCustomers()
      {
         var customers = _repository.GetAll();
         return _mapper.Map(customers);
      }

      public void AddCustomer(CustomerContract customer)
      {
         _repository.Add(_mapper.Map(customer));
      }
   }
}