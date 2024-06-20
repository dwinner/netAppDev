namespace HandmadeInterp;

public class BinaryOperation : IElement
{
   public enum Type
   {
      Addition,
      Subtraction
   }

   public Type MyType { get; set; }

   public IElement? Left { get; set; }

   public IElement? Right { get; set; }

   public int Value
   {
      get
      {
         if (Left != null && Right != null)
         {
            switch (MyType)
            {
               case Type.Addition:
                  return Left.Value + Right.Value;
               case Type.Subtraction:
                  return Left.Value - Right.Value;
               default:
                  throw new ArgumentOutOfRangeException(nameof(MyType));
            }
         }

         return default;
      }
   }
}