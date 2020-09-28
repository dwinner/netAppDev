using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ComboBoxSample
{
   public partial class MainWindow
   {
      private bool _handled = true;

      public ICommand SelectionChangedCommand { get; } = new RelayCommand(o =>
      {
         Debug.WriteLine(o?.ToString() ?? "None");
      });

      public MainWindow()
      {
         InitializeComponent();
         DataContext = this;
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