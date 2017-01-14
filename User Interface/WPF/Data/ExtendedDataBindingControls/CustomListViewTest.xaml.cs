using System.Windows.Controls;

namespace DataBinding
{
   public partial class CustomListViewTest
   {
      public CustomListViewTest()
      {
         InitializeComponent();
         ProductListView.ItemsSource = App.StoreDb.GetProducts();
      }

      private void OnViewChanged(object sender, SelectionChangedEventArgs e)
      {
         var selectedItem = (ComboBoxItem) ViewComboBox.SelectedItem;
         ProductListView.View = (ViewBase) FindResource(selectedItem.Content);
      }
   }
}