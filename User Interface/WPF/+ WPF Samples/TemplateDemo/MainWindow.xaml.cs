using System.Windows;

namespace TemplateDemo
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

      private void OnStyledButton(object sender, RoutedEventArgs e)
      {
         new StyledButtonWindow().Show();
      }

      private void OnStyledListBox(object sender, RoutedEventArgs e)
      {
         new StyledListBoxWindow().Show();
      }
   }
}
