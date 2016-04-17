using System.Windows;

namespace AnimationDemo
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
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

      private void OnKeyframeAnimation(object sender, RoutedEventArgs e)
      {
         new KeyFrameWindow().Show();
      }

      private void OnEventTrigger(object sender, RoutedEventArgs e)
      {
         new EventTriggerWindow().Show();
      }
   }
}
