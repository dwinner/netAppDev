using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ScreenCapture
{
   public partial class ScreenCaptureForm : Form
   {
      private Image _image;

      public ScreenCaptureForm()
      {
         InitializeComponent();
      }

      private void buttonCapture_Click(object sender, EventArgs e)
      {
         Visible = false;
         //wait for window to be hidden so that we don't see our
         //own window in the screen capture
         Thread.Sleep(500);
         _image = CaptureScreen();
         pictureBox.Image = _image;
         Visible = true;
         buttonCopy.Enabled = true;
      }

      private static Image CaptureScreen()
      {
         //combine bounds of all screens
         var bounds = GetScreenBounds();

         var bitmap = new Bitmap(bounds.Width, bounds.Height);
         using (var graphics = Graphics.FromImage(bitmap))
         {
            graphics.CopyFromScreen(bounds.Location, new Point(0, 0), bounds.Size);
         }

         return bitmap;
      }

      private static Rectangle GetScreenBounds()
      {
         var rect = new Rectangle();
         return Screen.AllScreens.Aggregate(rect, (current, screen) => Rectangle.Union(current, screen.Bounds));
      }

      private void buttonCopy_Click(object sender, EventArgs e)
      {
         if (_image != null)
         {
            Clipboard.SetImage(_image);
         }
      }
   }
}