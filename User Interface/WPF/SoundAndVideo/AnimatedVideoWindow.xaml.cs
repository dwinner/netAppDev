using System;
using System.Windows;
using System.Windows.Media;

namespace SoundAndVideo
{
   public partial class AnimatedVideoWindow
   {
      public AnimatedVideoWindow()
      {
         InitializeComponent();
      }

      private void OnPlayViaCode(object sender, RoutedEventArgs e)
      {
         // Create the MediaPlayer.
         var player = new MediaPlayer();
         player.Open(new Uri("test.mpg", UriKind.Relative));

         // Create the VideoDrawing.
         var videoDrawing = new VideoDrawing
         {
            Rect = new Rect(150, 0, 100, 100),
            Player = player
         };

         // Assign the DrawingBrush.
         var brush = new DrawingBrush(videoDrawing);
         Background = brush;

         // Start playback.
         player.Play();
      }
   }
}