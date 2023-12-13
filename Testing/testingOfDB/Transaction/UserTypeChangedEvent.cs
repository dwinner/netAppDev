namespace Transaction;

public class UserTypeChangedEvent(int userId, UserType oldType, UserType newType) : IDomainEvent
{
   public int UserId { get; } = userId;

   public UserType OldType { get; } = oldType;

   public UserType NewType { get; } = newType;

   protected bool Equals(UserTypeChangedEvent other) => UserId == other.UserId && Equals(OldType, other.OldType);

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
         return (UserId * 397) ^ OldType.GetHashCode();
      }
   }
}