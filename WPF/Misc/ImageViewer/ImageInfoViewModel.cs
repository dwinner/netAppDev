using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImageViewer
{
   public class ImageInfoViewModel
   {
      public ImageInfoViewModel(BitmapImage image)
      {
         Image = image;
      }

      public BitmapImage Image { get; private set; }

      public string FileName
      {
         get { return Path.GetFileName(Image.UriSource.LocalPath); }
      }

      public int Width
      {
         get { return Image.PixelWidth; }
      }

      public int Height
      {
         get { return Image.PixelHeight; }
      }

      public IEnumerable<KeyValuePair<string, object>> AllProperties
      {
         get { return CreateProperties(); }
      }

      private IDictionary<string, object> CreateProperties()
      {
         var properties = new Dictionary<string, object>();
         properties["Width"] = Image.PixelWidth;
         properties["Height"] = Image.PixelHeight;
         properties["DpiX"] = Image.DpiX;
         properties["DpiY"] = Image.DpiY;
         properties["BitsPerPixel"] = Image.Format.BitsPerPixel;
         properties["Format"] = Image.Format.ToString();

         return properties;
      }
   }
}