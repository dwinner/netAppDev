using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FlickerFree
{
   internal class DrawPanel : Panel
   {
      private readonly List<MyShape> _shapes = new List<MyShape>();
      private MyShape _draggedShape;
      private Point _prevPoint;

      public new bool DoubleBuffered
      {
         get { return base.DoubleBuffered; }
         set { base.DoubleBuffered = value; }
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
         base.OnMouseMove(e);
         if (_draggedShape != null)
         {
            var offset = new Point(e.Location.X - _prevPoint.X, e.Location.Y - _prevPoint.Y);
            var location = _draggedShape.Location;
            location.Offset(offset);
            _draggedShape.Location = location;
            _prevPoint = e.Location;
            Refresh();
         }
      }

      protected override void OnMouseDown(MouseEventArgs e)
      {
         base.OnMouseDown(e);

         //go through backwards to hit test the shapes on top
         for (var i = _shapes.Count - 1; i >= 0; i--)
         {
            if (_shapes[i].HitTest(e.Location))
            {
               _prevPoint = e.Location;
               _draggedShape = _shapes[i];
               return;
            }
         }

         //create a new shape
         var myShape = new MyShape(e.Location);
         _shapes.Add(myShape);

         Refresh();
      }

      protected override void OnMouseUp(MouseEventArgs e)
      {
         _draggedShape = null;
         Refresh();
      }

      protected override void OnMouseDoubleClick(MouseEventArgs e)
      {
         base.OnMouseDoubleClick(e);

         _shapes.Clear();
         Refresh();
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);

         PaintBackground(e.Graphics);

         DrawShapes(e.Graphics);
      }

      private void DrawShapes(Graphics graphics)
      {
         foreach (var shape in _shapes)
         {
            shape.Draw(graphics);
         }
      }

      private void PaintBackground(Graphics graphics)
      {
         using (Brush brush = new LinearGradientBrush(ClientRectangle, Color.LightBlue, Color.LightGreen, 135))
         {
            graphics.FillRectangle(brush, ClientRectangle);
         }
      }
   }
}