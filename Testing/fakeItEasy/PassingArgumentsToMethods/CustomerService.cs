namespace PassingArgumentsToMethods;

public class CustomerService(ISendEmail emailSender, ICustomerRepository customerRepository)
{
   public void SendEmailToAllCustomers()
   {
      var customers = customerRepository.GetAllCustomers();
      foreach (var customer in customers)
      {
         emailSender.SendMail("acompany@somewhere.com", customer.Email, "subject", "body");
      }
   }
}