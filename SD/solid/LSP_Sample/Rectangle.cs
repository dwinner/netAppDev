namespace LSP_Sample;

// using a classic example
public class Rectangle
{
   public Rectangle()
   {
   }

   public Rectangle(int width, int height)
   {
      Width = width;
      Height = height;
   }
   //public int Width { get; set; }
   //public int Height { get; set; }

   public virtual int Width { get; set; }
   public virtual int Height { get; set; }

   public bool IsSquare => Width == Height;

   public int Area => Width * Height;

   public override string ToString() => $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
}