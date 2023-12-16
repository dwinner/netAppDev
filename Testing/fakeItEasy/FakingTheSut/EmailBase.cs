namespace FakingTheSut;

public abstract class EmailBase(ISendEmail emailProvider)
{
   protected void SendEmailToCustomers(string subject, string body, IEnumerable<Customer> customers)
   {
      foreach (var customer in customers)
      {
         emailProvider.SendMail(GetFromEmailAddress(), customer.Email, subject, body);
      }
   }

   protected virtual string GetFromEmailAddress() => "default@default.default"/*ConfigurationManager.AppSettings["DefaultFromAddress"]*/;
}