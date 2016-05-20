using System.Collections.Generic;
using System.Windows;
using StoreDatabase;

namespace DataBinding
{   
   public partial class BindToLinqFilteredCollection
   {
      //bool isDirty;

      ICollection<Product> products;

      public BindToLinqFilteredCollection()
      {
         InitializeComponent();
      }

      void OnGetProducts(object sender, RoutedEventArgs e)
      {
         products = App.StoreDb.GetProductsFilteredWithLinq(decimal.Parse(txtMinimumCost.Text));
         lstProducts.ItemsSource = products;
      }

      void OnDeleteProduct(object sender, RoutedEventArgs e)
      {
         products.Remove((Product) lstProducts.SelectedItem);
      }

      void OnAddProduct(object sender, RoutedEventArgs e)
      {
         products.Add(new Product("00000", "?", 0, "?"));
      }

      void OnTextChanged(object sender, RoutedEventArgs e)
      {
         //isDirty = true;
      }

      void OnSelectionChanged(object sender, RoutedEventArgs e)
      {
         //isDirty = false;
      }
   }
}