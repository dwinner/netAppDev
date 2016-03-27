namespace Variance
{
   public class Shape
   {
      public double Width { get; set; }

      public double Height { get; set; }

      public override string ToString()
      {
         return string.Format("Width: {0}, Height: {1}", Width, Height);
      }
   }
}