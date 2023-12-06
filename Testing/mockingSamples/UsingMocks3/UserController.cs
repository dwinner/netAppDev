namespace UsingMocks3;

public class UserController
{
   public void RenameUser(int userId, string newName)
   {
      var user = GetUserFromDatabase(userId);
      user.Name = newName;
      SaveUserToDatabase(user);
   }

   private void SaveUserToDatabase(User user)
   {
   }

   private User GetUserFromDatabase(int userId) => new();
}