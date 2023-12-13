namespace Refactored_3;

public class User
{
   public User(int userId, string email, UserType type)
   {
      UserId = userId;
      Email = email;
      Type = type;
   }

   public int UserId { get; private set; }
   public string Email { get; private set; }
   public UserType Type { get; private set; }

   public void ChangeEmail(string newEmail, Company company)
   {
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