

// This box has not changed its meaning since the last tutorial
namespace UseOfPipelines;

/// <summary>
///    A box can hold 1 thing only
/// </summary>
/// <typeparam name="T">The type of the thing</typeparam>
public class Box<T>
{
   private T _item = default!;

   public bool IsEmpty = true;

   public Box(T newItem)
   {
      Item = newItem;
      IsEmpty = false;
   }

   public Box()
   {
   }

   public T Item
   {
      get => _item;
      set
      {
         _item = value;
         IsEmpty = false;
      }
   }
}