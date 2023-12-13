namespace Refactored_1;

public class UserController
{
   private readonly Database _database = new();
   private readonly MessageBus _messageBus = new();

   public void ChangeEmail(int userId, string newEmail)
   {
      var data = _database.GetUserById(userId);
      var email = (string)data[1];
      var type = (UserType)data[2];
      var user = new User(userId, email, type);

      var companyData = _database.GetCompany();
      var companyDomainName = (string)companyData[0];
      var numberOfEmployees = (int)companyData[1];

      var newNumberOfEmployees = user.ChangeEmail(
         newEmail, companyDomainName, numberOfEmployees);

      _database.SaveCompany(newNumberOfEmployees);
      _database.SaveUser(user);
      _messageBus.SendEmailChangedMessage(userId, newEmail);
   }
}