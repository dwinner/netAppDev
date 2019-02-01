using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Controls
{
   public partial class CheckBoxList
   {
      public CheckBoxList()
      {
         InitializeComponent();
      }

      private void OnSelectionChanged(object sender, RoutedEventArgs e)
      {         
         if (e.OriginalSource is CheckBox)
         {
            Lst.SelectedItem = e.OriginalSource;
         }

         if (Lst.SelectedItem == null)
            return;

         TxtSelection.Text = string.Format("You chose item at position {0}.\r\nChecked state is {1}.", Lst.SelectedIndex,
            ((CheckBox) Lst.SelectedItem).IsChecked);
      }

      private void OnCheckAllItems(object sender, RoutedEventArgs e)
      {
         TxtSelection.Text = Lst.Items.Cast<CheckBox>()
            .Where(item => item.IsChecked == true)
            .Aggregate(string.Empty,
               (current, item) => current + string.Format("{0} is checked.{1}", item.Content, Environment.NewLine));
      }
   }
}