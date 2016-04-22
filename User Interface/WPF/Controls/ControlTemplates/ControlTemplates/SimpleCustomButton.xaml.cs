using System.Windows;
using System.Windows.Controls;

namespace ControlTemplates
{
   public partial class SimpleCustomButton
   {
      public SimpleCustomButton()
      {
         InitializeComponent();
      }

      void Clicked(object sender, RoutedEventArgs e)
      {
         MessageBox.Show(string.Format("You clicked {0}", ((Button) sender).Name));
      }
   }
}