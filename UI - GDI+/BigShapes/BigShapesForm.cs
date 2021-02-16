using System.Drawing;
using System.Windows.Forms;

namespace BigShapes
{
   public partial class BigShapesForm : Form
   {
      private readonly Pen _bluePen = new Pen(Color.Blue, 3);
      private readonly Size _ellipseSize = new Size(200, 150);
      private readonly Point _ellipseTopLeftPoint = new Point(50, 200);
      private readonly Size _rectangleSize = new Size(200, 200);
      private readonly Point _rectangleTopLeftPoint = new Point(0, 0);
      private readonly Pen _redPen = new Pen(Color.Red, 2);

      public BigShapesForm()
      {
         InitializeComponent();
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);
         var deviceContext = e.Graphics;
         var scrollOffset = new Size(AutoScrollPosition);
         if (e.ClipRectangle.Top + scrollOffset.Width < AutoScrollMinSize.Height ||
             e.ClipRectangle.Left + scrollOffset.Height < AutoScrollMinSize.Width)
         {
            var rectangleArea = new Rectangle(_rectangleTopLeftPoint + scrollOffset, _rectangleSize);
            var ellipseArea = new Rectangle(_ellipseTopLeftPoint + scrollOffset, _ellipseSize);
            deviceContext.DrawRectangle(_bluePen, rectangleArea);
            deviceContext.DrawEllipse(_redPen, ellipseArea);
         }
      }
   }
}