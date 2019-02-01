using System.Windows.Input;

namespace RoutedEvents
{   
   public partial class OnlyNumbers
   {
      public OnlyNumbers()
      {
         InitializeComponent();
      }

      private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
      {
         short val;
         if (!short.TryParse(e.Text, out val))
         {
            e.Handled = true;
         }
      }

      private void OnPreviewKeyDown(object sender, KeyEventArgs e)
      {
         if (e.Key == Key.Space)
         {
            e.Handled = true;
         }
      }
   }
}