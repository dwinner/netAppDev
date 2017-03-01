using System;
using System.Drawing;

namespace ColorConverter
{
   internal abstract class Converter
   {
      /// <summary>
      ///    HSVs to RGB.
      /// </summary>
      /// <param name="hue">The hue (0-360).</param>
      /// <param name="saturation">The saturation (0-100).</param>
      /// <param name="value">The value (0-100).</param>
      /// <returns>The RGB color equivalent</returns>
      /// <remarks>
      ///    Based on code from "Building a Color Picker with GDI+ in Visual Basic.Net or C#" by Ken Getz
      ///    http://msdn.microsoft.com/en-us/magazine/cc164113.aspx
      /// </remarks>
      public static Color HsvToRgb(int hue, int saturation, int value)
      {
         double h = hue;
         var s = saturation/100.0;
         var v = value/100.0;

         double r = 0, g = 0, b = 0;
         if (Math.Abs(s) < double.Epsilon)
         {
            //no saturation = gray
            r = g = b = v;
         }
         else
         {
            var sector = h/60;
            var sectorNumber = (int) Math.Floor(sector);
            var sectorPart = sector - sectorNumber;

            //three axes of color
            var p = v*(1 - s);
            var q = v*(1 - (s*sectorPart));
            var t = v*(1 - (s*(1 - sectorPart)));

            switch (sectorNumber)
            {
               case 0: //dominated by red
                  r = v;
                  g = t;
                  b = p;
                  break;
               case 1: //dominated by green
                  r = q;
                  g = v;
                  b = p;
                  break;
               case 2:
                  r = p;
                  g = v;
                  b = t;
                  break;
               case 3: //dominated by blue
                  r = p;
                  g = q;
                  b = v;
                  break;
               case 4:
                  r = t;
                  g = p;
                  b = v;
                  break;
               case 5: //dominated by red
                  r = v;
                  g = p;
                  b = q;
                  break;
            }
         }
         return Color.FromArgb((int) (r*255), (int) (g*255), (int) (b*255));
      }

      /// <summary>
      ///    Covnert RGB to HSV
      /// </summary>
      /// <param name="rgbColor"></param>
      /// <param name="hue">The hue.</param>
      /// <param name="saturation">The saturation.</param>
      /// <param name="value">The value.</param>
      /// <remarks>
      ///    Based on code from "Building a Color Picker with GDI+ in Visual Basic.Net or C#" by Ken Getz
      ///    http://msdn.microsoft.com/en-us/magazine/cc164113.aspx
      /// </remarks>
      public static void RgbToHsv(Color rgbColor, out int hue, out int saturation, out int value)
      {
         var r = rgbColor.R/255.0;
         var g = rgbColor.G/255.0;
         var b = rgbColor.B/255.0;

         //get the min and max of all three components
         var min = Math.Min(Math.Min(r, g), b);
         var max = Math.Max(Math.Max(r, g), b);

         var v = max;
         var delta = max - min;
         double h, s;

         if (Math.Abs(max) < double.Epsilon || Math.Abs(delta) < double.Epsilon)
         {
            //we've either got black or gray
            s = h = 0;
         }
         else
         {
            s = delta/max;
            if (Math.Abs(r - max) < double.Epsilon)
            {
               h = (g - b)/delta;
            }
            else if (Math.Abs(g - max) < double.Epsilon)
            {
               h = 2 + (b - r)/delta;
            }
            else
            {
               h = 4 + (r - g)/delta;
            }
         }
         //scale h to 0 -360
         h *= 60;
         if (h < 0)
         {
            h += 360.0;
         }
         hue = (int) h;
         //scale saturation and value to 0-100
         saturation = (int) (s*100.0);
         value = (int) (v*100.0);
      }
   }
}