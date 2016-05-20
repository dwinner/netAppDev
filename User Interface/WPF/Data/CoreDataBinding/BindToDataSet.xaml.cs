using System.Data;
using System.Windows;

namespace DataBinding
{
   public partial class BindToDataSet
   {
      DataTable products;

      public BindToDataSet()
      {
         InitializeComponent();
      }

      void OnGetProducts(object sender, RoutedEventArgs e)
      {
         products = App.StoreDbDataSet.GetProducts();
         lstProducts.ItemsSource = products.DefaultView;
      }

      void OnDeleteProduct(object sender, RoutedEventArgs e)
      {
         ((DataRowView) lstProducts.SelectedItem).Row.Delete();
      }
   }
}