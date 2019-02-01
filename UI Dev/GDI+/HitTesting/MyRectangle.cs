using System.Drawing;

namespace HitTesting
{
   internal class MyRectangle : MyShape
   {
      private readonly int _height;
      private readonly int _left;
      private readonly int _top;
      private readonly int _width;

      public MyRectangle(int left, int top, int width, int height)
      {
         _left = left;
         _top = top;
         _width = width;
         _height = height;
      }

      public override void Draw(Graphics graphics)
      {
         graphics.DrawRectangle(Pens.Black, _left, _top, _width, _height);
      }

      public override bool HitTest(Point location)
      {
         return location.X >= _left && location.X <= _left + _width
                && location.Y >= _top && location.Y <= _top + _height;
      }

      public override string ToString()
      {
         return "MyRectangle";
      }
   }
}