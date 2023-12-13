namespace V1;

public class UserTypeChangedEvent : IDomainEvent
{
   public UserTypeChangedEvent(int userId, UserType oldType, UserType newType)
   {
      UserId = userId;
      OldType = oldType;
      NewType = newType;
   }

   public int UserId { get; }
   public UserType OldType { get; }
   public UserType NewType { get; }

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