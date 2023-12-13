namespace AfterIoC;

public interface ICustomerRepository
{
   Customer? GetCustomerBy(int customerId);
}