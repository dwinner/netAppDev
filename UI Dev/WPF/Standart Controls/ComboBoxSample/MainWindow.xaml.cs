using System.Windows;
using System.Windows.Controls;

namespace ComboBoxSample
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         var comboBox = (ComboBox) sender;
         var selectedItem = (ComboBoxItem) comboBox.SelectedItem;
         if (selectedItem.Content != null)
         {
            MessageBox.Show(selectedItem.Content.ToString());
         }
      }
   }
}