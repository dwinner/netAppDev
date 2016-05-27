using System.Collections.Generic;
using System.Windows;
using StoreDatabase;

namespace DataBinding
{
   public partial class MoreValueConverters
   {
      ICollection<Product> products;

      public MoreValueConverters()
      {
         InitializeComponent();
      }

      void OnGetProducts(object sender, RoutedEventArgs e)
      {
         products = App.StoreDb.GetProducts();
         lstProducts.ItemsSource = products;
      }
   }
}