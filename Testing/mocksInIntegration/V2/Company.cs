namespace V2;

public class Company
{
   public Company(string domainName, int numberOfEmployees)
   {
      DomainName = domainName;
      NumberOfEmployees = numberOfEmployees;
   }

   public string DomainName { get; }
   public int NumberOfEmployees { get; private set; }

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