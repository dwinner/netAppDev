namespace FakingTheSut.ProtectedPropertyGetterSetters;

public interface ICustomerRepository
{
   List<Customer> GetAllCustomersWithOrderTotalsOfOneHundredOrGreater();
}