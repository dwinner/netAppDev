using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NegativeImageAddIn
{
   public partial class ImagePreview
   {
      private readonly byte[] _originalPixels;
      private readonly BitmapSource _originalSource;

      public ImagePreview()
      {
         InitializeComponent();
      }

      public ImagePreview(Stream imageStream)
      {
         InitializeComponent();

         var imgSource = new BitmapImage();
         imgSource.BeginInit();
         imgSource.StreamSource = imageStream;
         imgSource.EndInit();
         ImageToProcess.Source = imgSource;


         _originalSource = (BitmapSource) ImageToProcess.Source;
         var stride = _originalSource.PixelWidth * _originalSource.Format.BitsPerPixel / 8;
         stride = stride + stride % 4 * 4;
         _originalPixels = new byte[stride * _originalSource.PixelHeight * _originalSource.Format.BitsPerPixel / 8];

         _originalSource.CopyPixels(_originalPixels, stride, 0);
      }

      private void OnIntensityChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
      {
         Cursor = Cursors.Wait;

         var changedPixels = ProcessImageBytes((byte[]) _originalPixels.Clone());

         var stride = _originalSource.PixelWidth * _originalSource.Format.BitsPerPixel / 8;
         var newSource = BitmapSource.Create(_originalSource.PixelWidth,
            _originalSource.PixelHeight, _originalSource.DpiX, _originalSource.DpiY,
            _originalSource.Format, _originalSource.Palette, changedPixels, stride);
         ImageToProcess.Source = newSource;

         Cursor = null;
      }

      private byte[] ProcessImageBytes(byte[] pixels)
      {
         var scaleFactor = IntensitySlider.Value / IntensitySlider.Maximum;
         for (var i = 0; i < pixels.Length - 2; i++)
         {
            pixels[i] = (byte) (255 * scaleFactor - pixels[i]);
            pixels[i + 1] = (byte) (255 * scaleFactor - pixels[i + 1]);
            pixels[i + 2] = (byte) (255 * scaleFactor - pixels[i + 2]);
         }

         return pixels;
      }
   }
}