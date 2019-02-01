using System;
using System.Windows;

namespace RoutedEvents
{   
   public partial class TunneledKeyPress
   {
      private int _eventCounter;
      private static readonly string Nl = Environment.NewLine;

      public TunneledKeyPress()
      {
         InitializeComponent();
      }

      private void SomeKeyPressed(object sender, RoutedEventArgs e)
      {
         _eventCounter++;
         var message = string.Format("#{0}:{1} Sender: {2}{1} Source: {3}{1} Original Source: {4}{1} Event: {5}",
            _eventCounter, Nl, sender, e.Source, e.OriginalSource, e.RoutedEvent);
         MessagesListBox.Items.Add(message);
         e.Handled = HandleCheckBox.IsChecked.HasValue && HandleCheckBox.IsChecked.Value;
      }

      private void OnClear(object sender, RoutedEventArgs e)
      {
         _eventCounter = 0;
         MessagesListBox.Items.Clear();
      }
   }
}