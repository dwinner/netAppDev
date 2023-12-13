namespace Transaction;

public class Company(string domainName, int numberOfEmployees)
{
   public string DomainName { get; } = domainName;

   public int NumberOfEmployees { get; private set; } = numberOfEmployees;

   public void ChangeNumberOfEmployees(int delta)
   {
      Precondition.Requires(NumberOfEmployees + delta >= 0);

      NumberOfEmployees += delta;
   }

   public bool IsEmailCorporate(string email)
   {
      var emailDomain = email.Split('@')[1];
      return emailDomain == DomainName;
   }
}