namespace FakingTheSut.ProtectedPropertyGetterSetters;

public abstract class EmailBase(ISendEmail emailProvider)
{
   protected abstract string FromEmailAddress { get; }

   protected void SendEmailToCustomers(string subject, string body, IEnumerable<Customer> customers)
   {
      foreach (var customer in customers)
      {
         emailProvider.SendMail(FromEmailAddress, customer.Email, subject, body);
      }
   }
}