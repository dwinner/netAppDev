using System.Collections.Generic;
using System.Windows;
using StoreDatabase;

namespace DataBinding
{
   public partial class BindToCollection
   {
      //bool isDirty;

      ICollection<Product> products;

      public BindToCollection()
      {
         InitializeComponent();
      }

      void OnGetProducts(object sender, RoutedEventArgs e)
      {
         products = App.StoreDb.GetProducts();
         lstProducts.ItemsSource = products;
      }

      void OnDeleteProduct(object sender, RoutedEventArgs e)
      {
         products.Remove((Product)lstProducts.SelectedItem);
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