using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Animation
{
   public partial class CustomEasingFunction : Window
   {
      public CustomEasingFunction()
      {
         InitializeComponent();
      }
   }

   public sealed class RandomJitterEase : EasingFunctionBase
   {
      public static readonly DependencyProperty JitterProperty =
         DependencyProperty.Register("Jitter", typeof(int), typeof(RandomJitterEase),
            new UIPropertyMetadata(1000), new ValidateValueCallback(ValidateJitter));
      
      private readonly Random _rand = new Random();

      public int Jitter
      {
         get { return (int)GetValue(JitterProperty); }
         set { SetValue(JitterProperty, value); }
      }

      protected override double EaseInCore(double normalizedTime)
      {
         return Math.Abs(normalizedTime - 1) < double.Epsilon
            ? 1
            : Math.Abs(normalizedTime - (double)_rand.Next(0, 10) / (2010 - Jitter));
      }

      private static bool ValidateJitter(object value)
      {
         var jitterValue = (int)value;
         return (jitterValue <= 2000) && (jitterValue >= 0);
      }
      
      protected override Freezable CreateInstanceCore()
      {
         return new RandomJitterEase();
      }
   }
}