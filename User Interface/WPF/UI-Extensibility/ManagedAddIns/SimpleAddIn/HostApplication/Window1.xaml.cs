using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HostView;

namespace ApplicationHost
{
   /// <summary>
   ///    Interaction logic for Window1.xaml
   /// </summary>
   public partial class Window1 : Window
   {
      public Window1()
      {
         InitializeComponent();
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         var path = Environment.CurrentDirectory;
         AddInStore.Update(path);

         IList<AddInToken> tokens = AddInStore.FindAddIns(typeof(ImageProcessorHostView), path);
         lstAddIns.ItemsSource = tokens;
      }

      private void cmdProcessImage_Click(object sender, RoutedEventArgs e)
      {
         var originalSource = (BitmapSource) img.Source;
         var stride = originalSource.PixelWidth * originalSource.Format.BitsPerPixel / 8;
         stride = stride + stride % 4 * 4;
         var originalPixels = new byte[stride * originalSource.PixelHeight * originalSource.Format.BitsPerPixel / 8];

         originalSource.CopyPixels(originalPixels, stride, 0);

         var token = (AddInToken) lstAddIns.SelectedItem;
         var addin = token.Activate<ImageProcessorHostView>(AddInSecurityLevel.Internet);
         var changedPixels = addin.ProcessImageBytes(originalPixels);

         var newSource = BitmapSource.Create(originalSource.PixelWidth, originalSource.PixelHeight, originalSource.DpiX,
            originalSource.DpiY, originalSource.Format, originalSource.Palette, changedPixels, stride);
         img.Source = newSource;
      }

      private void lstAddIns_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         cmdProcessImage.IsEnabled = lstAddIns.SelectedIndex != -1;
      }
   }
}