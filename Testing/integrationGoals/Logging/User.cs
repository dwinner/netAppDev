namespace Logging;

public class User(int userId, string email, UserType type, bool isEmailConfirmed)
{
   private Logger _logger;

   public int UserId { get; set; } = userId;

   public string Email { get; private set; } = email;

   public UserType Type { get; private set; } = type;

   public bool IsEmailConfirmed { get; } = isEmailConfirmed;

   public List<EmailChangedEvent> EmailChangedEvents { get; } = new();

   public string CanChangeEmail()
   {
      if (IsEmailConfirmed)
      {
         return "Can't change email after it's confirmed";
      }

      return null;
   }

   public void ChangeEmail(string newEmail, Company company)
   {
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
         _logger.Info(
            $"User {UserId} changed type " +
            $"from {Type} to {newType}");
      }

      Email = newEmail;
      Type = newType;
      EmailChangedEvents.Add(new EmailChangedEvent(UserId, newEmail));
   }
}