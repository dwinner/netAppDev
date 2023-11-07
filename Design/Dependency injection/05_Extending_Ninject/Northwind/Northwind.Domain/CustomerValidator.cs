using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.Domain
{
public class CustomerValidator : ICustomerValidator
{
    private readonly ICustomerRepository repository;

    public CustomerValidator(ICustomerRepository repository)
    {
        if (repository == null)
        {
            throw new ArgumentNullException("repository");
        }
        this.repository = repository;
    }

    public bool ValidateUniqueness(string customerID)
    {
        return repository.Get(customerID) == null;
    }
}
}
