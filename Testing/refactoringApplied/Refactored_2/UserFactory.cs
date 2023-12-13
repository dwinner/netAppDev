namespace Refactored_2;

public class UserFactory
{
   public static User Create(object[] data)
   {
      Precondition.Requires(data.Length >= 3);

      var id = (int)data[0];
      var email = (string)data[1];
      var type = (UserType)data[2];

      return new User(id, email, type);
   }
}