using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BitmapDirect
{
   public partial class BitmapDirectForm : Form
   {
      public BitmapDirectForm()
      {
         InitializeComponent();
         labelSize.Text = string.Format("{0:N0} x {1:N0}",
            pictureBoxSource.Image.Width, pictureBoxSource.Image.Height);
      }

      private void buttonCopySetPixel_Click(object sender, EventArgs e)
      {
         var watch = new Stopwatch();

         using (var sourceImg = new Bitmap(pictureBoxSource.Image))
         using (var destImg = new Bitmap(sourceImg.Width, sourceImg.Height))
         {
            watch.Start();

            for (var row = 0; row < sourceImg.Height; row++)
            {
               for (var col = 0; col < sourceImg.Width; col++)
               {
                  var color = sourceImg.GetPixel(col, row);
                  color = Color.FromArgb(color.R / 2, color.G / 2, color.B / 2);
                  destImg.SetPixel(col, row, color);
               }
            }

            watch.Stop();
            pictureBoxDest.Image = destImg;
            UpdateStatus(watch.Elapsed);
         }         
      }

      private void UpdateStatus(TimeSpan span)
      {
         labelStatus.Text = string.Format("Copied in {0}", span);
      }

      /// <summary>
      ///    Handles the Click event of the buttonCopyDirect control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
      private void buttonCopyDirect_Click(object sender, EventArgs e)
      {
         var watch = new Stopwatch();

         var sourceImg = new Bitmap(pictureBoxSource.Image);

         var destImg = new Bitmap(sourceImg.Width, sourceImg.Height);
         watch.Start();
         var dataRect = new Rectangle(0, 0, sourceImg.Width, sourceImg.Height);
         var sourceData = sourceImg.LockBits(dataRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
         var destData = destImg.LockBits(dataRect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

         var sourcePtr = sourceData.Scan0;
         var destPtr = destData.Scan0;
         var buffer = new byte[sourceData.Stride];

         for (var row = 0; row < sourceImg.Height; row++)
         {
            // yes, we could copy the whole bitmap in one go,
            // but want to demonstrate the point
            Marshal.Copy(
               sourcePtr, buffer, 0, sourceData.Stride);

            //fiddle with the bits
            for (var i = 0; i < buffer.Length; i += 4)
            {
               //each pixel is represented by 4 bytes
               //last byte is transparency, which we'll ignore
               buffer[i + 0] /= 2;
               buffer[i + 1] /= 2;
               buffer[i + 2] /= 2;
            }

            Marshal.Copy(
               buffer, 0, destPtr, destData.Stride);
            sourcePtr = new IntPtr(sourcePtr.ToInt64() + sourceData.Stride);
            destPtr = new IntPtr(destPtr.ToInt64() + destData.Stride);
         }
         sourceImg.UnlockBits(sourceData);
         destImg.UnlockBits(destData);
         watch.Stop();
         pictureBoxDest.Image = destImg;
         UpdateStatus(watch.Elapsed);
      }
   }
}