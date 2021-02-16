using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HostView;

namespace HostApplication
{
   public partial class HostAppWindow
   {
      public HostAppWindow()
      {
         InitializeComponent();
      }

      private void OnHostAppWindowLoaded(object sender, RoutedEventArgs e)
      {
         var addInPath = Environment.CurrentDirectory;
         AddInStore.Update(addInPath);
         IList<AddInToken> tokens = AddInStore.FindAddIns(typeof(ImageProcessorHostView), addInPath);
         AddInListBox.ItemsSource = tokens;
      }

      private void OnProcessImage(object sender, RoutedEventArgs e)
      {
         var originalSource = (BitmapSource)ImageToProcess.Source;
         var stride = originalSource.PixelWidth * originalSource.Format.BitsPerPixel / 8;
         stride = stride + stride % 4 * 4;
         var originalPixels = new byte[stride * originalSource.PixelHeight * originalSource.Format.BitsPerPixel / 8];

         originalSource.CopyPixels(originalPixels, stride, 0);

         var token = (AddInToken)AddInListBox.SelectedItem;
         var addin = token.Activate<ImageProcessorHostView>(AddInSecurityLevel.Internet);
         var changedPixels = addin.ProcessImageBytes(originalPixels);

         var newSource = BitmapSource.Create(
            originalSource.PixelWidth, originalSource.PixelHeight,
            originalSource.DpiX, originalSource.DpiY,
            originalSource.Format, originalSource.Palette, changedPixels, stride);
         ImageToProcess.Source = newSource;
      }

      private void OnAddInChanged(object sender, SelectionChangedEventArgs e)
      {
         ProcessImageButton.IsEnabled = AddInListBox.SelectedIndex != -1;
      }
   }
}