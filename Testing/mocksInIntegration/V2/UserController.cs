namespace V2;

public class UserController
{
   private readonly Database _database;
   private readonly EventDispatcher _eventDispatcher;

   public UserController(
      Database database,
      MessageBus messageBus,
      IDomainLogger domainLogger)
   {
      _database = database;
      _eventDispatcher = new EventDispatcher(
         messageBus, domainLogger);
   }

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
      _eventDispatcher.Dispatch(user.DomainEvents);

      return "OK";
   }
}