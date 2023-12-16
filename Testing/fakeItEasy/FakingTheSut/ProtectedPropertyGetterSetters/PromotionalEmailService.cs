namespace FakingTheSut.ProtectedPropertyGetterSetters;

public class PromotionalEmailService(ICustomerRepository customerRepository, ISendEmail emailProvider)
   : EmailBase(emailProvider)
{
   protected override string FromEmailAddress => "APromotionalEmail@somecompany.com";

   public void SendEmail(string subject, string body)
   {
      var customers = customerRepository.GetAllCustomersWithOrderTotalsOfOneHundredOrGreater();
      SendEmailToCustomers(subject, body, customers);
   }
}