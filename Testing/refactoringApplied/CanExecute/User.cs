namespace CanExecute;

public class User
{
   public User(int userId, string email, UserType type, bool isEmailConfirmed)
   {
      UserId = userId;
      Email = email;
      Type = type;
      IsEmailConfirmed = isEmailConfirmed;
   }

   public int UserId { get; private set; }
   public string Email { get; private set; }
   public UserType Type { get; private set; }
   public bool IsEmailConfirmed { get; }

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
      }

      Email = newEmail;
      Type = newType;
   }
}