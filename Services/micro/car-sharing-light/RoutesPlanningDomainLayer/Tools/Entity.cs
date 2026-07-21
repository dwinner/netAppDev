namespace RoutesPlanningDomainLayer.Tools;

public abstract class Entity<TKey>
   where TKey : IEquatable<TKey>
{
   public virtual TKey Id { get; protected set; } = default!;

   public bool IsTransient() => Equals(Id, default(TKey));

   #region domain events handling

   public List<IEventNotification> DomainEvents { get; private set; } = null!;

   public void AddDomainEvent(IEventNotification evt)
   {
      DomainEvents.Add(evt);
   }

   public void RemoveDomainEvent(IEventNotification evt)
   {
      DomainEvents.Remove(evt);
   }

   #endregion

   #region override Equal

   public override bool Equals(object? obj) =>
      obj is Entity<TKey> entity && Equals(entity);

   public bool Equals(Entity<TKey> other)
   {
      if (other.IsTransient() || IsTransient())
      {
         return false;
      }

      return Equals(Id, other.Id);
   }

   private int? _requestedHashCode;

   public override int GetHashCode()
   {
      if (!IsTransient())
      {
         _requestedHashCode ??= HashCode.Combine(Id);
         return _requestedHashCode.Value;
      }

      return base.GetHashCode();
   }

   public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
   {
      if (Equals(left, null))
      {
         return Equals(right, null);
      }

      return left.Equals(right);
   }

   public static bool operator !=(Entity<TKey> left, Entity<TKey> right) => !(left == right);

   #endregion
}