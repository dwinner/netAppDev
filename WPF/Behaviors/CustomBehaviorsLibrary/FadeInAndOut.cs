using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

namespace CustomBehaviorsLibrary
{
   public class FadeOutAction : TargetedTriggerAction<UIElement>
   {
      // The default fade out time is 2 seconds.
      public static readonly DependencyProperty DurationProperty =
         DependencyProperty.Register("Duration", typeof (TimeSpan),
            typeof (FadeOutAction), new PropertyMetadata(TimeSpan.FromSeconds(2)));

      private readonly DoubleAnimation _fadeAnimation = new DoubleAnimation();

      private readonly Storyboard _fadeStoryboard = new Storyboard();

      public FadeOutAction()
      {
         _fadeStoryboard.Children.Add(_fadeAnimation);
      }

      public TimeSpan Duration
      {
         get { return (TimeSpan) GetValue(DurationProperty); }
         set { SetValue(DurationProperty, value); }
      }

      protected override void Invoke(object args)
      {
         // Make sure the storyboard isn't already running.
         _fadeStoryboard.Stop();

         // Set up the storyboard.            
         Storyboard.SetTargetProperty(_fadeAnimation, new PropertyPath("Opacity"));
         Storyboard.SetTarget(_fadeAnimation, Target);

         // Set up the animation.
         // It's important to do this at the last possible instant,
         // in case the value for the Duration property changes.
         _fadeAnimation.To = 0;
         _fadeAnimation.Duration = Duration;

         _fadeStoryboard.Begin();
      }
   }

   public class FadeInAction : TargetedTriggerAction<UIElement>
   {
      // The default fade in is 0.5 seconds.
      public static readonly DependencyProperty DurationProperty =
         DependencyProperty.Register("Duration", typeof (TimeSpan),
            typeof (FadeInAction), new PropertyMetadata(TimeSpan.FromSeconds(0.5)));

      private readonly DoubleAnimation _fadeAnimation = new DoubleAnimation();

      private readonly Storyboard _fadeStoryboard = new Storyboard();

      public FadeInAction()
      {
         _fadeStoryboard.Children.Add(_fadeAnimation);
      }

      public TimeSpan Duration
      {
         get { return (TimeSpan) GetValue(DurationProperty); }
         set { SetValue(DurationProperty, value); }
      }

      protected override void Invoke(object args)
      {
         // Make sure the storyboard isn't already running.
         _fadeStoryboard.Stop();

         // Set up the storyboard.                        
         Storyboard.SetTargetProperty(_fadeAnimation, new PropertyPath("Opacity"));
         Storyboard.SetTarget(_fadeAnimation, this.Target);

         // Set up the animation.            
         _fadeAnimation.To = 1;
         _fadeAnimation.Duration = Duration;

         _fadeStoryboard.Begin();
      }
   }
}