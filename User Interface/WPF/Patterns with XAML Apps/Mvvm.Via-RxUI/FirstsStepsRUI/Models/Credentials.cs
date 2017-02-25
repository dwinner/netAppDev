namespace FirstsStepsRUI.Models
{
   public class Credential
   {
      public Credential(User user, string password)
      {
         User = user;
         Password = password;
      }

      public User User { get; private set; }
      public string Password { get; private set; }
   }
}