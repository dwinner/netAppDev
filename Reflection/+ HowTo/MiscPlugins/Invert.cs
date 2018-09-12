using System.Drawing;

namespace MiscPlugins
{
   /// <summary>
   /// Инверт-фильтр изображения
   /// </summary>
   public class Invert : PluginInterfaces.IImagePlugin
   {
      #region IImagePlugin Members

      public Image RunPlugin(Image image)
      {
         Bitmap newBitmap;
         using (Bitmap originalBitmap = new Bitmap(image))
         {
            newBitmap = new Bitmap(image);
                  
            for (int row = 0; row < originalBitmap.Height; ++row)
            {
               for (int col = 0; col < originalBitmap.Width; ++col)
               {
                  newBitmap.SetPixel(originalBitmap.Width - col - 1, originalBitmap.Height - row - 1,
                     originalBitmap.GetPixel(col, row));
               }
            }            
         }
         return newBitmap;
      }

      public string Name
      {
         get { return "Invert image"; }
      }

      #endregion
   }
}
