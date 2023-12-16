namespace OutAndRefParameters;

public interface ICustomerRepository
{
   void GetAllCustomers(out List<Customer> customers);
}