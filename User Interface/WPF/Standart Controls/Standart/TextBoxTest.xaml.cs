using System.Windows;

namespace Controls
{
   public partial class TextBoxTest
   {
      public TextBoxTest()
      {
         InitializeComponent();
      }

      private void OnSelectionChanged(object sender, RoutedEventArgs e)
      {
         if (TxtSelection == null)
            return;

         TxtSelection.Text = string.Format("Selection from {0} to {1} is \"{2}\"", Txt.SelectionStart,
            Txt.SelectionLength, Txt.SelectedText);
      }
   }
}