using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.Win32;

namespace FileInputBoxSample
{
   [ContentProperty(nameof(FileName))]
   public partial class FileInputBoxUserControl
   {
	   public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(nameof(FileName),
		   typeof(string), typeof(FileInputBoxUserControl), new PropertyMetadata(default(string)));

	   public static readonly RoutedEvent FileNameChangedEvent = EventManager.RegisterRoutedEvent(nameof(FileNameChanged),
		   RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FileInputBoxUserControl));

      public FileInputBoxUserControl()
      {
         InitializeComponent();
         FileTextBox.TextChanged += OnTextChanged;
      }

      public string FileName
      {
         get { return (string) GetValue(FileNameProperty); }
         set { SetValue(FileNameProperty, value); }
      }

      private void OnTextChanged(object sender, TextChangedEventArgs e)
      {
         e.Handled = true;
         var args = new RoutedEventArgs(FileNameChangedEvent);
         RaiseEvent(args);
      }

      private void OnBrowse(object sender, RoutedEventArgs e)
      {
         var dialog = new OpenFileDialog();
         if (dialog.ShowDialog() == true)
            FileName = dialog.FileName;
      }

      public event RoutedEventHandler FileNameChanged
      {
         add { AddHandler(FileNameChangedEvent, value); }
         remove { RemoveHandler(FileNameChangedEvent, value); }
      }
   }
}