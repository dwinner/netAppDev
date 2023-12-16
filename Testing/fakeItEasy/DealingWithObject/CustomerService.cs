namespace DealingWithObject;

public class CustomerService(ISendEmail emailSender, ICustomerRepository customerRepository)
{
   public void SendEmailToPreferredCustomers()
   {
      var customers = customerRepository.GetAllCustomers();
      foreach (var customer in customers.Where(customer => customer.IsPreferred))
      {
         emailSender.SendMail(new Email
         {
            EmailType = new PreferredCustomerEmail
            {
               Email = customer.Email
            }
         });
      }
   }
}