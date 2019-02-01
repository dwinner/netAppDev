// Сохранение моментального снимка экрана

using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ScreenSnapshotSample
{
   internal static class Program
   {
      private const string SnapshotFile = "snapshot.png";

      private static void Main()
      {
         GetScreenSnapshot(SnapshotFile);
      }

      private static void GetScreenSnapshot(string snapshotFile)
      {
         using (
            var screenCaptureBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
               Screen.PrimaryScreen.Bounds.Height))
         using (var gContext = Graphics.FromImage(screenCaptureBitmap))
         {
            gContext.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0,
               screenCaptureBitmap.Size, CopyPixelOperation.SourceCopy);
            screenCaptureBitmap.Save(snapshotFile, ImageFormat.Png);
         }
      }
   }
}