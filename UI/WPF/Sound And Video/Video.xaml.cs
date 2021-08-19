using System;
using System.Windows;

namespace SoundAndVideo
{
   public partial class Video
   {
      public Video()
      {
         InitializeComponent();
      }

      private void OnPlay(object sender, RoutedEventArgs e)
      {
         VideoEl.Position = TimeSpan.Zero;
         VideoEl.Play();
      }
   }
}