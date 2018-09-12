using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrawToBitmap
{
   public partial class DrawToBitmapForm : Form
   {
      public DrawToBitmapForm()
      {
         InitializeComponent();
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);
         Render(e.Graphics);
      }

      private static void Render(Graphics graphics)
      {
         graphics.FillEllipse(Brushes.Red, 10, 10, 100, 50);
      }

      private void button1_Click(object sender, EventArgs e)
      {
         using (var bitmap = new Bitmap(ClientSize.Width, ClientSize.Height))
         using (var graphics = Graphics.FromImage(bitmap))
         {
            Render(graphics);
            Clipboard.SetImage(bitmap);
         }
      }
   }
}