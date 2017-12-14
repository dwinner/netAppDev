using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnapToGrid
{
   public sealed partial class MainForm : Form
   {
      private Point _firstPoint = Point.Empty;
      private Point _secondPoint = Point.Empty;

      public MainForm()
      {
         InitializeComponent();
         DoubleBuffered = true;
      }

      protected override void OnMouseDown(MouseEventArgs e)
      {
         base.OnMouseDown(e);

         _firstPoint = _secondPoint = SnapInput(e.Location);
         Capture = true;
      }

      protected override void OnMouseUp(MouseEventArgs e)
      {
         base.OnMouseUp(e);
         Capture = false;
         Refresh();
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
         base.OnMouseMove(e);
         if (!Capture)
            return;
         _secondPoint = SnapInput(e.Location);
         Refresh();
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);
         if (!Capture)
            return;

         var topLeft = new Point(Math.Min(_firstPoint.X, _secondPoint.X), Math.Min(_firstPoint.Y, _secondPoint.Y));
         var bottomRight = new Point(Math.Max(_firstPoint.X, _secondPoint.X), Math.Max(_firstPoint.Y, _secondPoint.Y));
         var size = new Size(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
         var rect = new Rectangle(topLeft, size);
         e.Graphics.DrawRectangle(Pens.Black, rect);
      }

      private static Point SnapInput(Point point)
      {
         const int gridSize = 25;
         return new Point(SnapInput(point.X, gridSize), SnapInput(point.Y, gridSize));
      }

      private static int SnapInput(double input, int multiple) => (int) (input + multiple / 2.0) / multiple * multiple;
   }
}