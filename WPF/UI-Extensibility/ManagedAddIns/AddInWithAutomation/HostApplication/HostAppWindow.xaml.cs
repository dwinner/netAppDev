using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using HostView;

namespace HostApplication
{
   public partial class HostAppWindow
   {
      private ImageProcessorHostView _addin;
      private AutomationHost _automationHost;
      private byte[] _originalPixels;

      // NOTE: These variables are set on the UI thread and read on the background thread.
      // For better organization, wrap these in a custom class, and pass that an instance
      // of that class to RunBackgroundAddIn() method using the ParameterizedThreadStart
      // delegate.
      private BitmapSource _originalSource;
      private int _stride;

      public HostAppWindow()
      {
         InitializeComponent();
      }

      private void OnHostAppWindowLoaded(object sender, RoutedEventArgs e)
      {
         var path = Environment.CurrentDirectory;
         AddInStore.Update(path);

         IList<AddInToken> tokens = AddInStore.FindAddIns(typeof(ImageProcessorHostView), path);
         AddInListBox.ItemsSource = tokens;

         _automationHost = new AutomationHost(AddInProcessProgressBar);
      }

      private void OnProcessImage(object sender, RoutedEventArgs e)
      {
         // Get the byte array ready.
         _originalSource = (BitmapSource) ImageToProcess.Source;
         _stride = _originalSource.PixelWidth * _originalSource.Format.BitsPerPixel / 8;
         _stride = _stride + _stride % 4 * 4;
         _originalPixels = new byte[_stride * _originalSource.PixelHeight * _originalSource.Format.BitsPerPixel / 8];
         _originalSource.CopyPixels(_originalPixels, _stride, 0);

         // Start the add-in. 
         var token = (AddInToken) AddInListBox.SelectedItem;
         _addin = token.Activate<ImageProcessorHostView>(AddInSecurityLevel.Internet);
         _addin.Initialize(_automationHost);

         // Launch the image processing work on a separate thread.
         var thread = new Thread(RunBackgroundAddIn);
         thread.Start();
      }

      private void RunBackgroundAddIn()
      {
         // Do the work.
         var changedPixels = _addin.ProcessImageBytes(_originalPixels);

         // Update the UI on the UI thread.
         Dispatcher.BeginInvoke(DispatcherPriority.Normal,
            (ThreadStart) delegate
            {
               var newSource = BitmapSource.Create(
                  _originalSource.PixelWidth,
                  _originalSource.PixelHeight,
                  _originalSource.DpiX,
                  _originalSource.DpiY,
                  _originalSource.Format,
                  _originalSource.Palette,
                  changedPixels,
                  _stride);

               ImageToProcess.Source = newSource;
               AddInProcessProgressBar.Value = 0;

               // Release the add-in (it's a member variable now,
               // so this step is explicit).
               _addin = null;
            }
         );
      }

      private void OnAddInChanged(object sender, SelectionChangedEventArgs e)
      {
         ProcessImageButton.IsEnabled = AddInListBox.SelectedIndex != -1;
      }

      private sealed class AutomationHost : HostObject
      {
         private readonly ProgressBar _progressBar;

         public AutomationHost(ProgressBar progressBar)
         {
            _progressBar = progressBar;
         }

         public override void ReportProgress(int progressPercent)
         {
            // Update the UI on the UI thread.
            _progressBar.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               (ThreadStart) delegate { _progressBar.Value = progressPercent; }
            );
         }
      }
   }
}