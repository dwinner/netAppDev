using System;
using System.Drawing;
using System.Windows.Forms;

namespace HowToCSharp.Ch05.RadiansAndDegrees
{
   public sealed partial class RadiansAndDegreesForm : Form
   {
      public RadiansAndDegreesForm()
      {
         InitializeComponent();
         DoubleBuffered = true;
      }

      public string Radians { get; set; }

      public string Degrees { get; set; }

      protected override void OnPaint(PaintEventArgs e)
      {
         DrawAxes(e.Graphics);
         DrawValues(e.Graphics);
      }

      protected override void OnSizeChanged(EventArgs e)
      {
         Refresh();
      }

      private void DrawAxes(Graphics graphics)
      {
         int midX = ClientSize.Width / 2;
         int midY = ClientSize.Height / 2;

         const int border = 10;

         graphics.DrawLine(Pens.Black, new Point(0 + border, midY), new Point(ClientSize.Width - border, midY));
         graphics.DrawLine(Pens.Black, new Point(midX, 0 + border), new Point(midX, ClientSize.Height - border));
      }

      private void DrawValues(Graphics graphics)
      {
         if (!string.IsNullOrEmpty(Radians))
         {
            graphics.DrawString(Radians, DefaultFont, Brushes.Blue, 5, 0);
         }

         if (!string.IsNullOrEmpty(Degrees))
         {
            graphics.DrawString(Degrees, DefaultFont, Brushes.Red, 5, 15);
         }
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
         Point center = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
         Point mouse = e.Location;

         double radians = Math.Atan2(mouse.Y - center.Y, mouse.X - center.X);
         double degrees = RadiansToDegrees(radians);
         radians = DegreesToRadians(degrees);

         Radians = string.Format("{0:F3} radians", radians);
         Degrees = string.Format("{0:F3} degrees", degrees);

         base.OnMouseMove(e);
         Refresh();
      }

      private static double RadiansToDegrees(double radians)
      {
         return radians * 360.0 / (2.0 * Math.PI);
      }

      private static double DegreesToRadians(double degrees)
      {
         return degrees * (2.0 * Math.PI) / 360.0;
      }
   }
}
