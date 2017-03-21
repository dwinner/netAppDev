using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HostView;

namespace HostApplication
{
   public partial class AppHostWindow
   {
      private ImageProcessorHostView _addIn;

      public AppHostWindow()
      {
         InitializeComponent();
      }

      private void OnAppHostWindowLoaded(object sender, RoutedEventArgs e)
      {
         var path = Environment.CurrentDirectory;
         AddInStore.Update(path);

         IList<AddInToken> tokens = AddInStore.FindAddIns(typeof(ImageProcessorHostView), path);
         AddInListBox.ItemsSource = tokens;
      }

      private void OnProcessImage(object sender, RoutedEventArgs e)
      {
         var token = (AddInToken) AddInListBox.SelectedItem;
         _addIn = token.Activate<ImageProcessorHostView>(AddInSecurityLevel.Host);
         var streamResourceInfo = Application.GetResourceStream(new Uri("Forest.jpg", UriKind.RelativeOrAbsolute));
         if (streamResourceInfo != null)
         {
            var imageStream = streamResourceInfo.Stream;
            AddInBorder.Child = _addIn.GetVisual(imageStream);
         }
      }

      private void OnAddInChanged(object sender, SelectionChangedEventArgs e)
      {
         ProcessImageButton.IsEnabled = AddInListBox.SelectedIndex != -1;
      }
   }
}