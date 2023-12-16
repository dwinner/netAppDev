namespace DoingNothing;

public class CustomerService(ISendEmail emailSender, ICustomerRepository customerRepository)
{
   public void SendEmailToAllCustomersAsWellAsDoSomethingElse()
   {
      var customers = customerRepository.GetAllCustomers();
      foreach (var customer in customers)
      {
         //although this call is being made, we don't care about the setup, b/c it doesn't directly affect our results
         emailSender.SendMail();
      }
   }
}