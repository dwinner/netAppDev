using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

using CustomerInterface;

namespace CustomerService
{

    public class CustomerServiceImplementation : ICustomer
    {

        #region ICustomer Members

        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public int RegisterCustomer(CustomerInterface.Customer customer)
        {
            Console.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name);

            using (DataClassesCustomerDataContext ctx = new DataClassesCustomerDataContext())
            {

                Customer customerToInsert;
                customerToInsert = new Customer();
                customerToInsert.CustomerName = customer.CustomerName;
                customerToInsert.CustomerFirstName = customer.CustomerFirstName;
                ctx.Customers.InsertOnSubmit(customerToInsert);
                ctx.SubmitChanges();
                Console.WriteLine(">SubmitChanges RegisterCustomer ");
                return customerToInsert.CustomerID;
            }
        }

        #endregion
    }
}


