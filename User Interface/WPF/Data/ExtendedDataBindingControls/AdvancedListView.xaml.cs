using System.Windows;

namespace DataBinding
{
   public partial class AdvancedListView
   {
      public AdvancedListView()
      {
         InitializeComponent();
         ProductListView.ItemsSource = App.StoreDb.GetProducts();
      }

      private void OnGridViewColumnClick(object sender, RoutedEventArgs e)
      {
      }
   }
}