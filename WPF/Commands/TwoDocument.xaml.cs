using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Commands
{
   public partial class TwoDocument
   {
      private readonly Dictionary<object, bool> _isDirty = new Dictionary<object, bool>();

      public TwoDocument()
      {
         InitializeComponent();
      }

      private void SaveCommand(object sender, ExecutedRoutedEventArgs e)
      {
         var text = ((TextBox)sender).Text;
         MessageBox.Show(string.Format("About to save: {0}", text));
         _isDirty[sender] = false;
      }

      private void txt_TextChanged(object sender, RoutedEventArgs e)
      {
         _isDirty[sender] = true;
      }

      private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
      {
         e.CanExecute = _isDirty.ContainsKey(sender) && _isDirty[sender];
      }
   }
}