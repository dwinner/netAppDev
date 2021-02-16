using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorSelector
{
   internal static class UiUtilities
   {
      /// <summary>
      ///    Display the VB or С# code to implement the provided Color
      /// </summary>
      /// <param name="textBlock">Text block to render</param>
      /// <param name="color">The color to implement</param>
      /// <param name="isVb">True to generate VB; false for C#</param>
      internal static void DisplayCode(TextBlock textBlock, Color color, bool isVb)
      {
         var code = isVb ? "Dim color As Color = " : "Color color = ";
         code += $@"Color.FromArgb({color.R}, {color.G}, {color.B});";
         textBlock.Text = code;
         textBlock.Text = code;
      }

      /// <summary>
      ///    Given a Color struct, update the UI controls to show the RGB values,
      ///    and repeat the selected color within the BorderSelectedColor control
      /// </summary>
      /// <param name="border">Border</param>
      /// <param name="color">The current color under the mouse cursor</param>
      /// <param name="textBlock">Text block</param>
      internal static void DisplayColor(TextBlock textBlock, Border border, Color color)
      {
         // Set the border color to match the selected color
         var brush = new SolidColorBrush(color);
         border.Background = brush;

         // Display the RGB values
         var rgb = $"{color.R}, {color.G}, {color.B}";
         textBlock.Text = rgb;
      }

      /// <summary>
      ///    Returns a color structure representing the color of the pixel at the current mouse x and y coordinates
      /// </summary>
      /// <returns>A color structure</returns>
      internal static Color GetPointColor(FrameworkElement control)
      {
         // Retrieve the relative coordinate of the mouse position in relation to the current window
         var point = Mouse.GetPosition(control);

         // Grab a bitmap of the current window
         var renderTargetBitmap =
            new RenderTargetBitmap((int) control.ActualWidth, (int) control.ActualHeight, 96, 96, PixelFormats.Default);
         renderTargetBitmap.Render(control);

         // Determine if we are in bounds
         if ((point.X <= renderTargetBitmap.PixelWidth) && (point.Y <= renderTargetBitmap.PixelHeight))
         {
            // Crop a pixel out of the larger bitmap
            var croppedBitmap =
               new CroppedBitmap(renderTargetBitmap, new Int32Rect((int) point.X, (int) point.Y, 1, 1));

            // Copy the pixel to a byte array
            var pixels = new byte[4];
            croppedBitmap.CopyPixels(pixels, 4, 0);

            // Convert the RGB byte array to a Color structure
            var selectedColor = Color.FromRgb(pixels[2], pixels[1], pixels[0]);

            return selectedColor;
         }

         return Colors.Black;
      }
   }
}