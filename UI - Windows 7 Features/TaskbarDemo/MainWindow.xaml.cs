using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;

namespace TaskBarDemo
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         var videos = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
         DataContext = Directory.EnumerateFiles(videos);
      }

      private void OnPlay(object sender, ExecutedRoutedEventArgs e)
      {
         var startImageRes = TryFindResource("StartImage");
         var startImageSource = startImageRes as ImageSource;
         if (startImageSource != null)
            TaskbarItem.Overlay = startImageSource;
         PlayerMediaElement.Play();
      }

      private void OnStop(object sender, ExecutedRoutedEventArgs e)
      {
         var stopImageRes = TryFindResource("StopImage");
         var stopImageSource = stopImageRes as ImageSource;
         if (stopImageSource != null)
            TaskbarItem.Overlay = stopImageSource;
         PlayerMediaElement.Stop();
      }
   }
}