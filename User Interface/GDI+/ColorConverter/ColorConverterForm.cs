using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorConverter
{
   public partial class ColorConverterForm : Form
   {
      private bool _updating;

      public ColorConverterForm()
      {
         InitializeComponent();
         SetColorRanges();
         SetupHueColors();
      }

      private void SetupHueColors()
      {
         //we want hue slider to be a little different than others
         var colors = new Color[360];
         int h;
         const int s = 100;
         const int v = 100;
         for (h = 0; h < colors.Length; h++)
         {
            colors[h] = Converter.HsvToRgb(h, s, v);
         }
         colorSliderH.CustomColors = colors;
      }

      private void SetColorRanges()
      {
         var rgbColor = GetRgbColor();

         //RGB sliders
         colorSliderR.SetColors(
            Color.FromArgb(0, rgbColor.G, rgbColor.B),
            Color.FromArgb(255, rgbColor.G, rgbColor.B));

         colorSliderG.SetColors(
            Color.FromArgb(rgbColor.R, 0, rgbColor.B),
            Color.FromArgb(rgbColor.R, 255, rgbColor.B));

         colorSliderB.SetColors(
            Color.FromArgb(rgbColor.R, rgbColor.G, 0),
            Color.FromArgb(rgbColor.R, rgbColor.G, 255));

         //HSV sliders
         int h, s, v;
         Converter.RgbToHsv(rgbColor, out h, out s, out v);
         colorSliderS.SetColors(Converter.HsvToRgb(h, 0, v), Converter.HsvToRgb(h, 100, v));
         colorSliderV.SetColors(Converter.HsvToRgb(h, s, 0), Converter.HsvToRgb(h, s, 100));

         labelResult.BackColor = rgbColor;
      }

      private Color GetRgbColor()
      {
         return Color.FromArgb(colorSliderR.Value, colorSliderG.Value, colorSliderB.Value);
      }

      private void OnRgbValuesChanged(object sender, EventArgs e)
      {
         if (!_updating)
         {
            _updating = true;
            SetHsvValues();
            SetColorRanges();
            _updating = false;
         }
      }

      private void OnHsvValuesChanged(object sender, EventArgs e)
      {
         if (!_updating)
         {
            _updating = true;
            SetRgbValues();
            _updating = false;
            SetColorRanges();
         }
      }

      private void SetHsvValues()
      {
         var rgbColor = GetRgbColor();
         int h, s, v;
         Converter.RgbToHsv(rgbColor, out h, out s, out v);

         colorSliderH.Value = h;
         colorSliderS.Value = s;
         colorSliderV.Value = v;
      }

      private void SetRgbValues()
      {
         var h = colorSliderH.Value;
         var s = colorSliderS.Value;
         var v = colorSliderV.Value;

         var rgbColor = Converter.HsvToRgb(h, s, v);
         colorSliderR.Value = rgbColor.R;
         colorSliderG.Value = rgbColor.G;
         colorSliderB.Value = rgbColor.B;
      }
   }
}