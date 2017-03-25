using System.Windows;

namespace AnimationDemo
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnEllipseAnimation(object sender, RoutedEventArgs e)
      {
         new EllipseWindow().Show();
      }

      private void OnButtonAnimation(object sender, RoutedEventArgs e)
      {
         new ButtonAnimationWindow().Show();
      }

      private void OnKeyFrameAnimation(object sender, RoutedEventArgs e)
      {
         new KeyFrameWindow().Show();
      }

      private void OnEventTrigger(object sender, RoutedEventArgs e)
      {
         new EventTriggerWindow().Show();
      }
   }
}