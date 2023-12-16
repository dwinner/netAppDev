namespace ThatMatches;

public class CustomerService(ISendEmail emailSender, ICustomerRepository customerRepository)
{
   public void SendEmailToAllCustomers()
   {
      var customers = customerRepository.GetAllCustomers();
      foreach (var customer in customers)
      {
         emailSender.SendMail(new Email
         {
            From = "acompany@somewhere.com",
            To = customer.Email,
            Subject = "subject",
            Body = "body"
         });
      }
   }
}