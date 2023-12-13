namespace Refactored_3;

public class UserController
{
   private readonly Database _database = new();
   private readonly MessageBus _messageBus = new();

   public void ChangeEmail(int userId, string newEmail)
   {
      var userData = _database.GetUserById(userId);
      var user = UserFactory.Create(userData);

      var companyData = _database.GetCompany();
      var company = CompanyFactory.Create(companyData);

      user.ChangeEmail(newEmail, company);

      _database.SaveCompany(company);
      _database.SaveUser(user);
      _messageBus.SendEmailChangedMessage(userId, newEmail);
   }
}