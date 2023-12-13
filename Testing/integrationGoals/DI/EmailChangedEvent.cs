namespace DI;

public class EmailChangedEvent(int userId, string newEmail)
{
   public int UserId { get; } = userId;

   public string NewEmail { get; } = newEmail;

   protected bool Equals(EmailChangedEvent other) => UserId == other.UserId && string.Equals(NewEmail, other.NewEmail);

   public override bool Equals(object obj)
   {
      if (ReferenceEquals(null, obj))
      {
         return false;
      }

      if (ReferenceEquals(this, obj))
      {
         return true;
      }

      if (obj.GetType() != GetType())
      {
         return false;
      }

      return Equals((EmailChangedEvent)obj);
   }

   public override int GetHashCode()
   {
      unchecked
      {
         return (UserId * 397) ^ (NewEmail != null ? NewEmail.GetHashCode() : 0);
      }
   }
}