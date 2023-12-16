namespace FakingTheSut;

public class AdminEmailService(ICustomerRepository customerRepository, ISendEmail emailProvider)
   : EmailBase(emailProvider)
{
   public void SendPromotionalEmail(string subject, string body)
   {
      var customers = customerRepository.GetAllCustomersWithOrderTotalsOfOneHundredOrGreater();
      SendEmailToCustomers(subject, body, customers);
   }
}