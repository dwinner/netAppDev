using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RoutedEvents
{
   public partial class KeyPressEvents
   {
      public KeyPressEvents()
      {
         InitializeComponent();
      }

      private void OnKeyEvent(object sender, KeyEventArgs e)
      {
         if (IgnoreRepeatCheckbox.IsChecked.HasValue && IgnoreRepeatCheckbox.IsChecked.Value && e.IsRepeat)
            return;

         var message = //"At: " + e.Timestamp.ToString() +
            string.Format("Event: {0}. Key: {1}", e.RoutedEvent, e.Key);
         MessagesListBox.Items.Add(message);
      }

      private void OnTextInput(object sender, TextCompositionEventArgs e)
      {
         var message = //"At: " + e.Timestamp.ToString() +
            string.Format("Event: {0}. Text: {1}", e.RoutedEvent, e.Text);
         MessagesListBox.Items.Add(message);
      }

      private void OnTextChanged(object sender, TextChangedEventArgs e)
      {
         var message = string.Format("Event: {0}", e.RoutedEvent);
         MessagesListBox.Items.Add(message);
      }

      private void OnClear(object sender, RoutedEventArgs e)
      {
         MessagesListBox.Items.Clear();
      }
   }
}