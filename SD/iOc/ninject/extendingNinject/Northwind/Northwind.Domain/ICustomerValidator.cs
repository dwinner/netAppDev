using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.Domain
{
    public interface ICustomerValidator
    {
        bool ValidateUniqueness(string customerID);

    }
}
