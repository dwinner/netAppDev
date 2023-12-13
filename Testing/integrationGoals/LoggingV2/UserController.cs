namespace LoggingV2;

public class UserController
{
   private readonly Database _database = new();
   private readonly MessageBus _messageBus = new();

   public string ChangeEmail(int userId, string newEmail)
   {
      var userData = _database.GetUserById(userId);
      var user = UserFactory.Create(userData);

      var error = user.CanChangeEmail();
      if (error != null)
      {
         return error;
      }

      var companyData = _database.GetCompany();
      var company = CompanyFactory.Create(companyData);

      user.ChangeEmail(newEmail, company);

      _database.SaveCompany(company);
      _database.SaveUser(user);
      EventDispatcher.Dispatch(user.DomainEvents);

      return "OK";
   }
}