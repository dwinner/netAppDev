using Microsoft.Win32;
using System;
using System.Windows;

namespace MovieIn3D
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

      }

      private void OnSelectFile(object sender, RoutedEventArgs e)
      {
         OpenFileDialog ofd = new OpenFileDialog();
         if (ofd.ShowDialog() == true)
         {
            mediaPlayer.Source = new Uri(ofd.FileName);
            mediaPlayer.Play();
            mediaPlayer.Pause();
         }
      }

      void OnPlay(object sender, RoutedEventArgs e)
      {
         mediaPlayer.Play();
      }

      void OnPause(object sender, RoutedEventArgs e)
      {
         mediaPlayer.Pause();
      }

      void OnStop(object sender, RoutedEventArgs e)
      {
         mediaPlayer.Stop();
      }
   }
}
