using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Pens
{
   public partial class PensForm : Form
   {
      private readonly LinearGradientBrush _gradientBrush = new LinearGradientBrush(new Point(0, 0), new Point(50, 50),
         Color.Red, Color.Yellow);

      private readonly HatchBrush _hatchBrush = new HatchBrush(HatchStyle.DashedVertical, Color.Green, Color.Transparent);
      private readonly Pen[] _pens;

      private readonly Point[] _points =
      {
         new Point(5, 10),
         new Point(50, 10),
         new Point(10, 50)
      };

      public PensForm()
      {
         InitializeComponent();

         _pens = new[]
         {
            new Pen(Color.Red),
            new Pen(Color.Green, 4), //width 4 pen
            new Pen(Color.Purple, 2), //dash-dot pen
            new Pen(_gradientBrush, 6), //gradient pen
            new Pen(_gradientBrush, 6), //rounded join and end cap
            new Pen(_hatchBrush, 6) //hatch pen
         };

         _pens[2].DashStyle = DashStyle.DashDot;
         _pens[4].EndCap = LineCap.Round;
         _pens[4].LineJoin = LineJoin.Round;
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);

         const int boxWidth = 100;
         const int boxHeight = 100;

         for (var i = 0; i < _pens.Length; i++)
         {
            e.Graphics.TranslateTransform(
               (i%4)*boxWidth,
               (i/4)*boxHeight);
            e.Graphics.DrawLines(_pens[i], _points);
            e.Graphics.ResetTransform();
         }
      }

      /// <summary>
      ///    Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
            foreach (var pen in _pens)
            {
               pen.Dispose();
            }
            _hatchBrush.Dispose();
            _gradientBrush.Dispose();
         }

         base.Dispose(disposing);
      }
   }
}