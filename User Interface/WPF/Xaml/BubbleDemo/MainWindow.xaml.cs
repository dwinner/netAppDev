using System.Collections.ObjectModel;
using System.Windows;

namespace BubbleDemo
{
   public partial class MainWindow
   {
      private readonly ObservableCollection<string> _messages = new ObservableCollection<string>();

      public MainWindow()
      {
         InitializeComponent();
         DataContext = _messages;
      }

      private void AddMessage(string message, object sender, RoutedEventArgs e)
      {
         var senderElement = sender as FrameworkElement;
         var sourceElement = e.Source as FrameworkElement;
         var originalSourceElement = e.OriginalSource as FrameworkElement;

         if (senderElement != null && (sourceElement != null && originalSourceElement != null))
         {
            _messages.Add(string.Format("{0}, sender: {1}; source: {2}; original source: {3}", message,
               senderElement.Name, sourceElement.Name, originalSourceElement.Name));
         }
      }

      private void OnOuterButtonClick(object sender, RoutedEventArgs e)
      {
         AddMessage("outer event", sender, e);
      }

      private void OnSecondButtonClick(object sender, RoutedEventArgs e)
      {
         AddMessage("button2", sender, e);
         e.Source = sender;
      }

      private void OnFirstInnerButton(object sender, RoutedEventArgs e)
      {
         AddMessage("inner1", sender, e);
      }

      private void OnSecondInnerButton(object sender, RoutedEventArgs e)
      {
         AddMessage("inner2", sender, e);
         e.Handled = true;
      }
   }
}