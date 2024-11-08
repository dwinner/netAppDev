﻿using PluginInterfaces;
using System.Drawing;

namespace BluifyPlugin
{
   /// <summary>
   /// Blue-фильтр
   /// </summary>
   public class MakeBlue : IImagePlugin
   {
      #region IImagePlugin Members

      public Image RunPlugin(Image image)
      {
         Bitmap bitmap = new Bitmap(image);
         
         for (int row = 0; row < bitmap.Height; ++row)
         {
            for (int col = 0; col < bitmap.Width; ++col)
            {
               Color color = bitmap.GetPixel(col, row);
               if (color.B > 0)
               {
                  color = Color.FromArgb(color.A, color.R, color.G, 255);
               }
               bitmap.SetPixel(col, row, color);
            }
         }
         return bitmap;
      }

      public string Name
      {
         get
         {
            return "Make Blue";
         }
      }

      #endregion
   }
}
