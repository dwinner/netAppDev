namespace SampleProject;

public class User
{
   public int UserId { get; private set; }
   public string Email { get; private set; }
   public UserType Type { get; private set; }

   public void ChangeEmail(int userId, string newEmail)
   {
      var data = Database.GetUserById(userId);
      UserId = userId;
      Email = (string)data[1];
      Type = (UserType)data[2];

      if (Email == newEmail)
      {
         return;
      }

      //bool isEmailTaken = Database.GetUserByEmail(newEmail) != null;
      //if (isEmailTaken)
      //    return "Email is taken";

      var companyData = Database.GetCompany();
      var companyDomainName = (string)companyData[0];
      var numberOfEmployees = (int)companyData[1];

      var emailDomain = newEmail.Split('@')[1];
      var isEmailCorporate = emailDomain == companyDomainName;
      var newType = isEmailCorporate
         ? UserType.Employee
         : UserType.Customer;

      if (Type != newType)
      {
         var delta = newType == UserType.Employee ? 1 : -1;
         var newNumber = numberOfEmployees + delta;
         Database.SaveCompany(newNumber);
      }

      Email = newEmail;
      Type = newType;

      Database.SaveUser(this);
      MessageBus.SendEmailChangedMessage(UserId, newEmail);
   }
}