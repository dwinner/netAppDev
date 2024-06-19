namespace PropertyProxy;

public class Property<T>(T value, string name = "")
   where T : new()
{
   private T _value = value;

   public Property() : this(default)
   {
   }

   public T Value
   {
      get => _value;
      set
      {
         if (Equals(_value, value))
         {
            return;
         }

         Console.WriteLine($"Assigning {value} to {name}");
         _value = value;
      }
   }

   public static implicit operator T(Property<T> property) => property.Value; // int n = p_int;

   public static implicit operator Property<T>(T value) => new(value); // Property<int> p = 123;

   protected bool Equals(Property<T> other) => EqualityComparer<T>.Default.Equals(_value, other._value);

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

      return Equals((Property<T>)obj);
   }

   public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(_value);
}