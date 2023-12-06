namespace UsingMocks1;

public class Controller
{
   private readonly IDatabase _database;
   private readonly IEmailGateway _emailGateway;

   public Controller(IEmailGateway emailGateway) => _emailGateway = emailGateway;

   public Controller(IDatabase database) => _database = database;

   public void GreetUser(string userEmail)
   {
      _emailGateway.SendGreetingsEmail(userEmail);
   }

   public Report CreateReport()
   {
      var numberOfUsers = _database.GetNumberOfUsers();
      return new Report(numberOfUsers);
   }
}