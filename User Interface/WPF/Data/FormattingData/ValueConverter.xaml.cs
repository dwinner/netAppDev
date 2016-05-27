using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StoreDatabase;

namespace DataBinding
{
   public partial class ValueConverter
   {
      Product product;

      public ValueConverter()
      {
         InitializeComponent();
      }

      void OnGetProduct(object sender, RoutedEventArgs e)
      {
         int id;
         if (int.TryParse(txtId.Text, out id))
         {
            try
            {
               product = App.StoreDb.GetProduct(id);
               gridProductDetails.DataContext = product;
            }
            catch (Exception)
            {
               MessageBox.Show("Error contacting database.");
            }
         }
         else
         {
            MessageBox.Show("Invalid ID.");
         }
      }

      void OnUpdateProduct(object sender, RoutedEventArgs e)
      {
         // Make sure update has taken place.
         FocusManager.SetFocusedElement(this, (Button) sender);
         MessageBox.Show(product.UnitCost.ToString(CultureInfo.InvariantCulture));
      }
   }
}