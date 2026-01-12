namespace UseOfMonad;

/// <summary>
///    A box can hold a thing only
/// </summary>
/// <typeparam name="T">The type of the thing</typeparam>
public class Box<T>
{
   private readonly T? _item;

   public Box(T newItem)
   {
      Item = newItem;
      IsEmpty = false;
   }

   public Box() => _item = default;

   public bool IsEmpty { get; private set; } = true;

   public T? Item
   {
      get => _item;
      private init
      {
         _item = value;
         IsEmpty = false;
      }
   }
}