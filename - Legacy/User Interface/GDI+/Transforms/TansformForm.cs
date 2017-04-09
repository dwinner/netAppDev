using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Transforms
{
   public partial class TansformForm : Form
   {
      private readonly Font _font = new Font("Verdana", 16.0f);

      public TansformForm()
      {
         InitializeComponent();
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);

         var ellipseRect = new Rectangle(25, 25, 100, 50);

         // Rotation, angles are in degrees
         e.Graphics.RotateTransform(-15);
         e.Graphics.FillEllipse(Brushes.Red, ellipseRect);
         e.Graphics.ResetTransform();

         // Translation
         e.Graphics.TranslateTransform(0, 100);
         e.Graphics.FillEllipse(Brushes.Blue, ellipseRect);
         e.Graphics.ResetTransform();

         // Translation + Rotation
         // notice the order! it's important
         e.Graphics.TranslateTransform(100, 100);
         e.Graphics.RotateTransform(-15);
         e.Graphics.FillEllipse(Brushes.Purple, ellipseRect);
         e.Graphics.ResetTransform();

         // Scale (and translation)
         e.Graphics.TranslateTransform(0, 200);
         //make it twice as long and 3/4 as wide
         e.Graphics.ScaleTransform(2.0f, 0.75f);
         e.Graphics.FillEllipse(Brushes.Green, ellipseRect);
         e.Graphics.ResetTransform();

         // we can also use any arbitrary matrix 
         // to transform the graphics
         var matrix = new Matrix();
         matrix.Translate(0, 300);
         matrix.Shear(0.5f, 0.25f);
         e.Graphics.Transform = matrix;
         e.Graphics.DrawString("Hello, Shear", _font, Brushes.Black, 0, 0);
         e.Graphics.ResetTransform();
      }
   }
}