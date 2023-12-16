namespace OutAndRefParameters;

public class CustomerService(ISendEmail emailSender, ICustomerRepository customerRepository)
{
   public void SendEmailToAllCustomers()
   {
      customerRepository.GetAllCustomers(out var customers);
      foreach (var customer in customers)
      {
         emailSender.SendMail();
      }
   }
}