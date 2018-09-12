using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Animation
{
   public partial class AnimationPlayer
   {
      public AnimationPlayer()
      {
         InitializeComponent();
      }

      private void OnCurrentTimeInvalidated(object sender, EventArgs e)
      {
         var storyboardClock = (Clock) sender;

         if (storyboardClock.CurrentProgress == null)
         {
            LblTime.Text = "[[ stopped ]]";
            ProgressBar.Value = 0;
         }
         else
         {
            LblTime.Text = storyboardClock.CurrentTime.ToString();
            ProgressBar.Value = (double) storyboardClock.CurrentProgress;
         }
      }

      private void OnSpeedValueChanged(object sender, RoutedEventArgs e)
      {
         FadeStoryboard.SetSpeedRatio(this, SpeedSlider.Value);
      }
   }
}