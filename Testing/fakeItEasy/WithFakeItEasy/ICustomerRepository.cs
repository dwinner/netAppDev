namespace WithFakeItEasy;

public interface ICustomerRepository
{
   Customer? GetCustomerBy(int customerId);
}