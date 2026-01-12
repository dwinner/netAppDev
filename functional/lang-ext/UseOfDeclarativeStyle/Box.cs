namespace UseOfDeclarativeStyle;

/// <summary>
///    A box can hold 1 thing only
/// </summary>
/// <typeparam name="T">The type of the thing</typeparam>
public class Box<T>
{
   private T _extract;

   public bool IsEmpty = true;

   public Box(T newExtract)
   {
      Extract = newExtract;
      IsEmpty = false;
   }

   public Box()
   {
   }

   public T Extract
   {
      get => _extract;
      set
      {
         _extract = value;
         IsEmpty = false;
      }
   }
}