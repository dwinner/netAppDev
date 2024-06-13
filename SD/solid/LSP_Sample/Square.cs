namespace LSP_Sample;

public class Square : Rectangle
{
   public Square(int side) => Width = Height = side;

   //public new int Width
   //{
   //  set { base.Width = base.Height = value; }
   //}

   //public new int Height
   //{ 
   //  set { base.Width = base.Height = value; }
   //}

   public override int Width // nasty side effects
   {
      set => base.Width = base.Height = value;
   }

   public override int Height
   {
      set => base.Width = base.Height = value;
   }
}