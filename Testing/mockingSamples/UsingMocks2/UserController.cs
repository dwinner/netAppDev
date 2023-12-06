namespace UsingMocks2;

public class UserController
{
   public void RenameUser(int userId, string newName)
   {
      var user = GetUserFromDatabase(userId);

      var normalizedName = user.NormalizeName(newName);
      user.Name = normalizedName;

      SaveUserToDatabase(user);
   }

   private void SaveUserToDatabase(User user)
   {
   }

   private User GetUserFromDatabase(int userId) => new();
}