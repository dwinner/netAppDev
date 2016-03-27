using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Brushes.Properties;

namespace Brushes
{
   public partial class BrushesForm : Form
   {
      private const int BoxSize = 175;
      private readonly Brush[] _brushes;
      private readonly GraphicsPath _path = new GraphicsPath();
      private Rectangle _ellipseRect = new Rectangle(0, 0, BoxSize, BoxSize);

      public BrushesForm()
      {
         InitializeComponent();

         _path.AddRectangle(_ellipseRect);

         _brushes = new Brush[]
         {
            new SolidBrush(Color.Red),
            new HatchBrush(HatchStyle.Cross, Color.Green, Color.Transparent),
            new TextureBrush(Resources.Elements),
            new LinearGradientBrush(_ellipseRect, Color.LightGoldenrodYellow, Color.ForestGreen, 45),
            new PathGradientBrush(_path)
         };

         var pathGradientBrush = _brushes[4] as PathGradientBrush;
         if (pathGradientBrush != null)
            pathGradientBrush.SurroundColors = new[] {Color.ForestGreen, Color.AliceBlue, Color.Aqua};
         var gradientBrush = _brushes[4] as PathGradientBrush;
         if (gradientBrush != null) gradientBrush.CenterColor = Color.Fuchsia;
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);

         _ellipseRect.Inflate(-10, -10);

         for (var i = 0; i < _brushes.Length; i++)
         {
            e.Graphics.TranslateTransform(
               (i%3)*BoxSize,
               (i/3)*BoxSize);
            e.Graphics.FillEllipse(_brushes[i], _ellipseRect);
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
            foreach (var brush in _brushes)
            {
               brush.Dispose();
            }
            _path.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}