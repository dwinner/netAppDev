using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StoreDatabase;

namespace DataBinding
{
   public partial class BindingGroupValidation
   {
      ICollection<Product> products;

      public BindingGroupValidation()
      {
         InitializeComponent();
      }

      void OnGetProducts(object sender, RoutedEventArgs e)
      {
         products = App.StoreDb.GetProducts();
         lstProducts.ItemsSource = products;
      }

      void OnUpdateProduct(object sender, RoutedEventArgs e)
      {
         // Make sure update has taken place.
         FocusManager.SetFocusedElement(this, (Button) sender);
      }

      void OnLostFocus(object sender, RoutedEventArgs e)
      {
         productBindingGroup.CommitEdit();
      }

      void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         productBindingGroup.CommitEdit();
      }
   }
}