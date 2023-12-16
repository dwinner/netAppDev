namespace IsInstanceOf;

public class CustomerService(IBus bus)
{
   public void CreateCustomer(string firstName, string lastName, string email)
   {
      bus.Send(new CreateCustomer { FirstName = firstName, LastName = lastName, Email = email });
   }
}