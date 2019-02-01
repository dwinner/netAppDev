using System;
using System.Drawing;

namespace FlickerFree
{
   internal class MyShape
   {
      private static readonly Random Random = new Random();
      private readonly Color _fillColor;
      private Rectangle _bounds;

      public MyShape(Point location)
      {
         _bounds = new Rectangle(location, new Size(50, 50));
         _fillColor = CreateRandomColor();
      }

      public Point Location
      {
         get { return _bounds.Location; }
         set { _bounds.Location = value; }
      }

      private static Color CreateRandomColor()
      {
         return Color.FromArgb(Random.Next(0, 256), Random.Next(0, 256), Random.Next(0, 256));
      }

      public bool HitTest(Point location)
      {
         return _bounds.Contains(location);
      }

      public void Draw(Graphics graphics)
      {
         using (Brush brush = new SolidBrush(_fillColor))
         {
            graphics.FillRectangle(brush, _bounds);
            graphics.DrawRectangle(Pens.Black, _bounds);
         }
      }
   }
}