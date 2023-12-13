namespace Refactored_2;

public class User
{
   public User(int userId, string email, UserType type)
   {
      UserId = userId;
      Email = email;
      Type = type;
   }

   public int UserId { get; private set; }
   public string Email { get; private set; }
   public UserType Type { get; private set; }

   public int ChangeEmail(string newEmail,
      string companyDomainName, int numberOfEmployees)
   {
      if (Email == newEmail)
      {
         return numberOfEmployees;
      }

      var emailDomain = newEmail.Split('@')[1];
      var isEmailCorporate = emailDomain == companyDomainName;
      var newType = isEmailCorporate
         ? UserType.Employee
         : UserType.Customer;

      if (Type != newType)
      {
         var delta = newType == UserType.Employee ? 1 : -1;
         var newNumber = numberOfEmployees + delta;
         numberOfEmployees = newNumber;
      }

      Email = newEmail;
      Type = newType;

      return numberOfEmployees;
   }
}