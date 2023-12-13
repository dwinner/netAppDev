namespace AfterIoC;

public class CustomerRepositoryStub : ICustomerRepository
{
   public Customer? GetCustomerBy(int customerId) => new() { Id = customerId };
}