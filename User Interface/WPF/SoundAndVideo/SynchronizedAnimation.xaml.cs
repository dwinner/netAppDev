using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace SoundAndVideo
{
   public partial class SynchronizedAnimation
   {
      private bool _suppressSeek;

      public SynchronizedAnimation()
      {
         InitializeComponent();
      }

      private void OnSliderPositionChanged(object sender, RoutedEventArgs e)
      {
         if (!_suppressSeek)
            mediaStoryboard.Storyboard.Seek(
               DocumentRoot, TimeSpan.FromSeconds(PositionSlider.Value), TimeSeekOrigin.BeginTime);
      }

      private void OnMediaOpened(object sender, RoutedEventArgs e)
      {
         PositionSlider.Maximum = Media.NaturalDuration.TimeSpan.TotalSeconds;
      }

      private void OnCurrentTimeInvalidated(object sender, EventArgs e)
      {
         // Sender is the clock that was created for this storyboard.
         var storyboardClock = (Clock) sender;

         if (storyboardClock.CurrentProgress == null)
         {
            TimeLabel.Text = "[[ stopped ]]";
         }
         else
         {
            TimeLabel.Text = string.Format("Position: {0}", storyboardClock.CurrentTime);
            _suppressSeek = true;
            if (storyboardClock.CurrentTime != null)
               PositionSlider.Value = storyboardClock.CurrentTime.Value.TotalSeconds;
            _suppressSeek = false;
         }
      }
   }
}