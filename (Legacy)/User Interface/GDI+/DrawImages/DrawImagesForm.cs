using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using DrawImages.Properties;

namespace DrawImages
{
   public partial class DrawImagesForm : Form
   {
      private InterpolationMode _interpolationMode = InterpolationMode.Default;

      public DrawImagesForm()
      {
         InitializeComponent();

         drawPanel.Paint += drawPanel_Paint;

         radioButtonInterpolationBicubic.Tag = InterpolationMode.Bicubic;
         radioButtonInterpolationBilinear.Tag = InterpolationMode.Bilinear;
         radioButtonInterpolationDefault.Tag = InterpolationMode.Default;
         radioButtonInterpolationHigh.Tag = InterpolationMode.High;
         radioButtonInterpolationHighQualityBicubic.Tag = InterpolationMode.HighQualityBicubic;
         radioButtonInterpolationHighQualityBilinear.Tag = InterpolationMode.HighQualityBilinear;
         radioButtonInterpolationLow.Tag = InterpolationMode.Low;
         radioButtonInterpolationNeighbor.Tag = InterpolationMode.NearestNeighbor;
      }

      private void drawPanel_Paint(object sender, PaintEventArgs e)
      {
         Image smallImage = Resources.Elements_Small;
         Image largeImage = Resources.Elements_Large;

         //draw normally
         e.Graphics.DrawImage(smallImage, 10, 10);

         //draw resized--interpolating pixels
         e.Graphics.InterpolationMode = _interpolationMode;
         e.Graphics.DrawImage(smallImage, 250, 100, 400, 400);

         //draw a subsection
         var sourceRect = new Rectangle(400, 400, 200, 200);
         var destRect = new Rectangle(10, 200, sourceRect.Width, sourceRect.Height);
         e.Graphics.DrawImage(Resources.Elements_Large, destRect, sourceRect, GraphicsUnit.Pixel);

         //draw same subsection with black as transparent
         var imageAttributes = new ImageAttributes();
         imageAttributes.SetColorKey(Color.Black, Color.Black, ColorAdjustType.Bitmap);
         destRect.Offset(200, 150);
         e.Graphics.DrawImage(largeImage, destRect, sourceRect.X, sourceRect.Y,
            sourceRect.Width, sourceRect.Height, GraphicsUnit.Pixel, imageAttributes);
      }

      private void OnInterpolationChanged(object sender, EventArgs e)
      {
         var button = sender as RadioButton;
         if (button != null)
         {
            _interpolationMode = (InterpolationMode) button.Tag;
            Refresh();
         }
      }
   }
}