using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Animation
{
   public partial class CodeAnimation
   {
      public CodeAnimation()
      {
         InitializeComponent();
      }

      private void OnGrow(object sender, RoutedEventArgs e)
      {
         var widthAnimation = new DoubleAnimation
         {
            To = Width - 30,
            Duration = TimeSpan.FromSeconds(5)
         };
         widthAnimation.Completed += OnAnimationCompleted;

         var heightAnimation = new DoubleAnimation
         {
            To = (Height - 50) / 3,
            Duration = TimeSpan.FromSeconds(5)
         };

         GrowButton.BeginAnimation(WidthProperty, widthAnimation);
         GrowButton.BeginAnimation(HeightProperty, heightAnimation);
      }

      private void OnAnimationCompleted(object sender, EventArgs e)
      {
         //double currentWidth = GrowButton.Width;
         //GrowButton.BeginAnimation(Button.WidthProperty, null);
         //GrowButton.Width = currentWidth;

         //MessageBox.Show("Completed!");
      }

      private void OnShrink(object sender, RoutedEventArgs e)
      {
         var widthAnimation = new DoubleAnimation { Duration = TimeSpan.FromSeconds(5) };
         var heightAnimation = new DoubleAnimation { Duration = TimeSpan.FromSeconds(5) };

         GrowButton.BeginAnimation(WidthProperty, widthAnimation);
         GrowButton.BeginAnimation(HeightProperty, heightAnimation);
      }

      private void OnGrowIncrementally(object sender, RoutedEventArgs e)
      {
         var widthAnimation = new DoubleAnimation
         {
            By = 10,
            Duration = TimeSpan.FromSeconds(0.5)
         };

         GrowIncrementallyButton.BeginAnimation(WidthProperty, widthAnimation);
      }
   }
}