using System.Drawing;
using System.Windows.Forms;

namespace DrawingShapes
{
   public partial class DrawingShapes : Form
   {
      public DrawingShapes()
      {
         InitializeComponent();
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);
         var graphics = e.Graphics;
         if (e.ClipRectangle.Top < 132 && e.ClipRectangle.Left < 82)
         {
            var bluePen = new Pen(Color.Blue, 3);
            graphics.DrawRectangle(bluePen, 0, 0, 50, 50);
            var redPen = new Pen(Color.Red, 2);
            graphics.DrawEllipse(redPen, 0, 50, 80, 60);
         }
      }
   }
}