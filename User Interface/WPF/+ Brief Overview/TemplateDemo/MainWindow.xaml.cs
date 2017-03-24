using System.Windows;

namespace TemplateDemo
{
   public partial class MainWindow
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