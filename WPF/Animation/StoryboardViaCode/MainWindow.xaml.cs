/**
 * Комбинированная раскадровка в коде
 */

using System;
using System.Windows;
using System.Windows.Media.Animation;
using AnimationStory = System.Windows.Media.Animation.Storyboard;

namespace AnimationSequence.ViaCode
{
   public partial class MainWindow
   {
      private static readonly TimeSpan DefaultDuration = TimeSpan.FromSeconds(2);
      private readonly ScaleAnimationHelper _scaleAnimation;

      public MainWindow()
      {
         InitializeComponent();
         _scaleAnimation = new ScaleAnimationHelper(FirstButton, DefaultDuration);
      }

      private void OnStart(object sender, RoutedEventArgs e)
      {
         _scaleAnimation.Go(true);
      }

      private void OnEnd(object sender, RoutedEventArgs e)
      {
         _scaleAnimation.Go(false);
      }

      private sealed class ScaleAnimationHelper
      {
         private const double DefaultStartValue = 0d;
         private const double DefaultEndValue = 1d;         

         private readonly TimeSpan _animationDuration;
         private readonly DependencyObject _dependencyObject;
         private readonly double _endValue;
         private readonly double _startValue;

         public ScaleAnimationHelper(DependencyObject dependencyObject, TimeSpan animationDuration,
            double startValue = DefaultStartValue, double endValue = DefaultEndValue)
         {
            _dependencyObject = dependencyObject;
            _animationDuration = animationDuration;
            _startValue = startValue;
            _endValue = endValue;
         }

         public void Go(bool @in, Action completedAction = null)
         {
            var combinedStoryboard = new AnimationStory();
            var start = @in ? _startValue : _endValue;
            var end = @in ? _endValue : _startValue;
            IEasingFunction easingFunction = new CircleEase
            {
               EasingMode = @in ? EasingMode.EaseIn : EasingMode.EaseOut               
            };

            var opacityAnimation = new DoubleAnimation(start, end, _animationDuration) { EasingFunction = easingFunction };
            AnimationStory.SetTarget(opacityAnimation, _dependencyObject);
            AnimationStory.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            combinedStoryboard.Children.Add(opacityAnimation);

            var scaleAnimationX = new DoubleAnimation(start, end, _animationDuration) { EasingFunction = easingFunction };
            AnimationStory.SetTarget(scaleAnimationX, _dependencyObject);
            AnimationStory.SetTargetProperty(scaleAnimationX, new PropertyPath("LayoutTransform.Children[0].ScaleX"));
            combinedStoryboard.Children.Add(scaleAnimationX);

            var scaleAnimationY = new DoubleAnimation(start, end, _animationDuration);
            AnimationStory.SetTarget(scaleAnimationY, _dependencyObject);
            AnimationStory.SetTargetProperty(scaleAnimationY, new PropertyPath("LayoutTransform.Children[0].ScaleY"));
            combinedStoryboard.Children.Add(scaleAnimationY);

            combinedStoryboard.Duration = _animationDuration;
            if (completedAction != null)
            {
               combinedStoryboard.Completed += (o, args) => completedAction();
            }

            combinedStoryboard.Begin();
         }
      }
   }
}