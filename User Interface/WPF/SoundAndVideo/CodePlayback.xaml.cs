using System;
using System.Windows;

namespace SoundAndVideo
{
   public partial class CodePlayback
   {
      public CodePlayback()
      {
         InitializeComponent();
      }

      private void OnPlay(object sender, RoutedEventArgs e)
      {
         Media.Play();
      }

      private void OnPause(object sender, RoutedEventArgs e)
      {
         Media.Pause();
      }

      private void OnStop(object sender, RoutedEventArgs e)
      {
         Media.Stop();
         Media.SpeedRatio = 1;
      }

      private void OnMediaOpened(object sender, RoutedEventArgs e)
      {
         SliderPosition.Maximum = Media.NaturalDuration.TimeSpan.TotalSeconds;
      }

      private void OnSliderPositionChanged(object sender, RoutedEventArgs e)
      {
         Media.Pause();
         Media.Position = TimeSpan.FromSeconds(SliderPosition.Value);
         Media.Play();
      }
   }
}