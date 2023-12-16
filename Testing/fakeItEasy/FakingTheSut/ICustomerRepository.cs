namespace FakingTheSut;

public interface ICustomerRepository
{
   List<Customer> GetAllCustomersWithOrderTotalsOfOneHundredOrGreater();
}