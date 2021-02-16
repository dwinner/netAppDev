using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AssemblyResources
{   
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnPlay(object sender, RoutedEventArgs e)
      {
         Img.Source = new BitmapImage(new Uri("images/winter.jpg", UriKind.Relative));
         //Img.Source = new BitmapImage(new Uri("pack://application:,,,/images/winter.jpg"));
         Sound.Stop();
         Sound.Play();
      }
   }
}