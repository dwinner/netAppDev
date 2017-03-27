using System.Windows;

namespace ValidationDemo
{
   public partial class MainWindow
   {
      private readonly SomeData _dataToBeValidated = new SomeData {Value1 = 11};

      public MainWindow()
      {
         InitializeComponent();
         DataContext = _dataToBeValidated;
      }

      private void OnShowValue(object sender, RoutedEventArgs e)
         => MessageBox.Show(_dataToBeValidated.Value1.ToString());

      private void OnShowNotification(object sender, RoutedEventArgs e) => new NotificationWindow().ShowDialog();
   }
}