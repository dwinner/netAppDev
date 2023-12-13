namespace DI;

public class User(int userId, string email, UserType type, bool isEmailConfirmed)
{
   private static readonly ILogger _logger = LogManager.GetLogger(typeof(User));

   private IDomainLogger _domainLogger;

   public int UserId { get; set; } = userId;

   public string Email { get; private set; } = email;

   public UserType Type { get; private set; } = type;

   public bool IsEmailConfirmed { get; } = isEmailConfirmed;

   public List<EmailChangedEvent> DomainEvents { get; } = new();

   public string CanChangeEmail()
   {
      if (IsEmailConfirmed)
      {
         return "Can't change email after it's confirmed";
      }

      return null;
   }

   public void ChangeEmail(
      string newEmail, Company company, ILogger logger)
   {
      logger.Info(
         $"Changing email for user {UserId} to {newEmail}");

      Precondition.Requires(CanChangeEmail() == null);

      if (Email == newEmail)
      {
         return;
      }

      var newType = company.IsEmailCorporate(newEmail)
         ? UserType.Employee
         : UserType.Customer;

      if (Type != newType)
      {
         var delta = newType == UserType.Employee ? 1 : -1;
         company.ChangeNumberOfEmployees(delta);
         AddDomainEvent(
            new UserTypeChangedEvent(UserId, Type, newType));
      }

      Email = newEmail;
      Type = newType;
      AddDomainEvent(new EmailChangedEvent(UserId, newEmail));

      logger.Info($"Email is changed for user {UserId}");
   }

   private void AddDomainEvent(EmailChangedEvent userTypeChangedEvent)
   {
   }

   private void AddDomainEvent(UserTypeChangedEvent userTypeChangedEvent)
   {
   }
}