using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ScrollMoreShapes
{
   public partial class MoreShapesForm : Form
   {
      private static readonly Brush BrickBrush =
         new HatchBrush(HatchStyle.DiagonalBrick, Color.DarkGoldenrod, Color.Cyan);

      private readonly Pen _bluePen = new Pen(Color.Blue, 3);
      private readonly Pen _brickWidePen = new Pen(BrickBrush, 10);
      private readonly Pen _redPen = new Pen(Color.Red, 2);
      private readonly Brush _solidAzureBrush = Brushes.Azure;
      private readonly Brush _solidYellowBrush = new SolidBrush(Color.Yellow);
      private Rectangle _boundsEllipse = new Rectangle(new Point(50, 200), new Size(200, 150));
      private Rectangle _boundsRectangle = new Rectangle(new Point(0, 0), new Size(200, 200));

      public MoreShapesForm()
      {
         InitializeComponent();
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);
         var graphics = e.Graphics;
         var scrollPosition = AutoScrollPosition;
         var scrollMinSize = AutoScrollMinSize;
         if (e.ClipRectangle.Top + scrollPosition.X < scrollMinSize.Width ||
             e.ClipRectangle.Left + scrollPosition.Y < scrollMinSize.Height)
         {
            graphics.DrawRectangle(_bluePen, _boundsRectangle);
            graphics.FillRectangle(_solidYellowBrush, _boundsRectangle);
            graphics.DrawEllipse(_redPen, _boundsEllipse);
            graphics.FillEllipse(_solidAzureBrush, _boundsEllipse);
            graphics.DrawLine(_brickWidePen, _boundsRectangle.Location, _boundsEllipse.Location + _boundsEllipse.Size);
         }
      }
   }
}