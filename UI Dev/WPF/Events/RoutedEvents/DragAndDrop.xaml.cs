using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RoutedEvents
{
   public partial class DragAndDrop
   {
      public DragAndDrop()
      {
         InitializeComponent();
      }

      private void lblSource_MouseDown(object sender, MouseButtonEventArgs e)
      {
         var lbl = (Label)sender;
         DragDrop.DoDragDrop(lbl, lbl.Content, DragDropEffects.Copy);
      }

      private void lblTarget_Drop(object sender, DragEventArgs e)
      {
         ((Label)sender).Content = e.Data.GetData(DataFormats.Text);
      }

      private void lblTarget_DragEnter(object sender, DragEventArgs e)
      {
         e.Effects = e.Data.GetDataPresent(DataFormats.Text) ? DragDropEffects.Copy : DragDropEffects.None;
      }
   }
}