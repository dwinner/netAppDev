using System.Windows;
using System.Windows.Navigation;

namespace NavigationApplication
{
   public partial class EmbeddedPage
   {
      public EmbeddedPage()
      {
         InitializeComponent();
      }

      private void OnCheckOwnsJournal(object sender, RoutedEventArgs e)
      {
         EmbeddedFrame.JournalOwnership = OwnsJournalCheckBox.IsChecked == true
            ? JournalOwnership.OwnsJournal
            : JournalOwnership.UsesParentJournal;
      }
   }
}