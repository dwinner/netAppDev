namespace AfterIoC;

public class CustomerService(ICustomerRepository customerRepository)
{
   public Customer? GetCustomerByCustomerId(int customerId) => customerRepository.GetCustomerBy(customerId);
}