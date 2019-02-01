using System.Windows;

namespace SingleInstanceApplication
{
   public partial class DocumentList
   {
      public DocumentList()
      {
         InitializeComponent();

         // Show the window names in a list.
         DocumentListBox.DisplayMemberPath = "Name";
         DocumentListBox.ItemsSource = ((WpfApplication) Application.Current).Documents;
      }
   }
}