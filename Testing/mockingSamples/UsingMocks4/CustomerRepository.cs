namespace UsingMocks4;

internal class CustomerRepository
{
   public Customer GetById(int customerId) => new();
}