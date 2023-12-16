namespace ReturnsNextFromSequence;

public class CustomerService(
   ISendEmail emailSender, ICustomerRepository customerRepository, IProvideNewGuids guidProvider)
{
   public void SendEmailToAllCustomers()
   {
      var customers = customerRepository.GetAllCustomers();
      foreach (var email in customers.Select(customer => new Email
               {
                  Id = guidProvider.GenerateNewId(),
                  To = customer.Email
               }))
      {
         emailSender.SendMail(email);
      }
   }
}