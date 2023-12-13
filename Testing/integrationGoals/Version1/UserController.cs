namespace Version1;

public class UserController
{
   private readonly Database _database;
   private readonly IMessageBus _messageBus;

   public UserController(Database database, IMessageBus messageBus)
   {
      _database = database;
      _messageBus = messageBus;
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
      foreach (var ev in user.EmailChangedEvents)
      {
         _messageBus.SendEmailChangedMessage(ev.UserId, ev.NewEmail);
      }

      return "OK";
   }
}