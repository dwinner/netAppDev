using System;

namespace Northwind.Core
{
   public class CustomerValidator : ICustomerValidator
   {
      private readonly ICustomerRepository _repository;

      public CustomerValidator(ICustomerRepository repository) =>
         _repository = repository ?? throw new ArgumentNullException(nameof(repository));

      public bool ValidateUniqueness(string customerId) => _repository.Get(customerId) == null;
   }
}