using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BitmapFromVisual
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         SaveImage(TestButton, 200, 200, "TestButton.png");
      }

      private static void SaveImage(Visual aVisual, int aWidth, int aHeight, string aFilePath)
      {
         var bitmap = new RenderTargetBitmap(aWidth, aHeight, 96, 96, PixelFormats.Pbgra32);
         bitmap.Render(aVisual);
         var encoder = new PngBitmapEncoder();
         encoder.Frames.Add(BitmapFrame.Create(bitmap));
         using (var stream = File.Create(aFilePath))
         {
            encoder.Save(stream);
         }
      }
   }
}