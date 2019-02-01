using System.Windows;

namespace LayoutPanels
{
   public partial class LocalizableText
   {
      public LocalizableText()
      {
         InitializeComponent();
      }

      private void chkLongText_Checked(object sender, RoutedEventArgs e)
      {
         PrevButton.Content = " <- Go to the Previous Window ";
         NextButton.Content = " Go to the Next Window -> ";
      }

      private void chkLongText_Unchecked(object sender, RoutedEventArgs e)
      {
         PrevButton.Content = "Prev";
         NextButton.Content = "Next";
      }
   }
}