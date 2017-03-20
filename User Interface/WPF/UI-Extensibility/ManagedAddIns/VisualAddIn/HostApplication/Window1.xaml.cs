using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ApplicationHost
{
   /// <summary>
   /// Interaction logic for Window1.xaml
   /// </summary>
   public partial class Window1 : Window
   {
      public Window1()
      {
         InitializeComponent();
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         string path = Environment.CurrentDirectory;
         AddInStore.Update(path);

         IList<AddInToken> tokens = AddInStore.FindAddIns(typeof(HostView.ImageProcessorHostView), path);
         lstAddIns.ItemsSource = tokens;
      }

      private HostView.ImageProcessorHostView addin;
      private void cmdProcessImage_Click(object sender, RoutedEventArgs e)
      {

         AddInToken token = (AddInToken)lstAddIns.SelectedItem;
         addin = token.Activate<HostView.ImageProcessorHostView>(AddInSecurityLevel.Host);

         Stream imageStream = Application.GetResourceStream(new Uri("Forest.jpg", UriKind.RelativeOrAbsolute)).Stream;

         pnlAddIn.Child = addin.GetVisual(imageStream);

      }

      private void lstAddIns_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         cmdProcessImage.IsEnabled = (lstAddIns.SelectedIndex != -1);
      }
   }
}
