using System.Windows;
using System.Windows.Input;

namespace RoutedEvents
{
   public partial class MousePosition
   {
      public MousePosition()
      {
         InitializeComponent();
      }

      private void CmdCapture_OnClick(object sender, RoutedEventArgs e)
      {
         AddHandler(Mouse.LostMouseCaptureEvent, new RoutedEventHandler(LostCapture));
         Mouse.Capture(Rect);

         CmdCapture.Content = "[ Mouse is now captured ... ]";
      }

      private void MouseMoved(object sender, MouseEventArgs e)
      {
         var pt = e.GetPosition(this);
         LblInfo.Text = string.Format("You are at ({0},{1}) in window coordinates", pt.X, pt.Y);
      }

      private void LostCapture(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("Lost capture");
         CmdCapture.Content = "Capture the Mouse";
      }
   }
}