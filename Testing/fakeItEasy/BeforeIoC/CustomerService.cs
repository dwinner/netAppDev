﻿namespace BeforeIoC;

public class CustomerService
{
   public Customer? GetCustomerByCustomerId(int customerId)
   {
      var customerRepository = new CustomerRepository();
      return customerRepository.GetCustomerBy(customerId);
   }
}