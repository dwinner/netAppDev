namespace MustNotHaveHappened;

public class CustomerService(ISendEmail emailSender, ICustomerRepository customerRepository)
{
   public void SendEmailToAllCustomers()
   {
      var customers = customerRepository.GetAllCustomers();
      foreach (var customer in customers)
      {
         emailSender.SendMail();
      }
   }
}