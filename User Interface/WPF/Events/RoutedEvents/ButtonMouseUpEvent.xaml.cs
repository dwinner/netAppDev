using System.Windows;

namespace RoutedEvents
{
   public partial class ButtonMouseUpEvent
   {
      public ButtonMouseUpEvent()
      {
         InitializeComponent();
         CmdButton.AddHandler(MouseUpEvent,
            new RoutedEventHandler((sender, e) => { MessageBox.Show("The (handled) Button.MouseUp event occurred."); }),
            true);
      }

      private void ButtonClick(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("The Button.Click event occurred. This may have been triggered with the keyboard.");
      }

      private void NeverCalled(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("You didn't see this message. That would be impossible.");
      }
   }
}