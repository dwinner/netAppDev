using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StylesAndResources
{
   public partial class ResourceDemoWindow
   {
      public ResourceDemoWindow()
      {
         InitializeComponent();
      }

      private void FirstButton_OnClick(object sender, RoutedEventArgs e)
      {
         var ctrl = sender as Control;
         if (ctrl != null)
            ctrl.Background = ctrl.FindResource("MyGradientBrush") as Brush;
      }

      private void SecondButton_OnClick(object sender, RoutedEventArgs e)
      {
         MyContainer.Resources.Clear();
         var brush = new LinearGradientBrush
         {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(0, 1),
            GradientStops = new GradientStopCollection
            {
               new GradientStop(Colors.White, 0.0),
               new GradientStop(Colors.Yellow, 0.14),
               new GradientStop(Colors.YellowGreen, 0.7)
            }
         };
         MyContainer.Resources.Add("MyGradientBrush", brush);
      }
   }
}