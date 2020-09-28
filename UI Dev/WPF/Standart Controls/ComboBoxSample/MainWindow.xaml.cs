using System;
using System.Windows;
using System.Windows.Controls;

namespace ComboBoxSample
{
   public partial class MainWindow
   {
      private bool _handled = true;

      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         if (sender is ComboBox box)
         {
            _handled = !box.IsDropDownOpen;
            Handle();
         }
      }

      private void OnDropDownClosed(object sender, EventArgs e)
      {
         if (_handled)
         {
            Handle();
         }

         _handled = true;
      }

      private void Handle()
      {
         var selectedItem = (ComboBoxItem) comboBox.SelectedItem;
         if (selectedItem.Content != null)
         {
            MessageBox.Show(selectedItem.Content.ToString());
         }
      }
   }
}