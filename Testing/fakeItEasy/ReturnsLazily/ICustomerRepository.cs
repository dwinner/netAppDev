namespace ReturnsLazily;

public interface ICustomerRepository
{
   Customer GetCustomerById(int id);
}