namespace Refactored_2;

public class UserController
{
   private readonly Database _database = new();
   private readonly MessageBus _messageBus = new();

   public void ChangeEmail(int userId, string newEmail)
   {
      var userData = _database.GetUserById(userId);
      var user = UserFactory.Create(userData);

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