using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StoreDatabase;

namespace DataBinding
{   
   public partial class EditProductObject
   {
      Product product;

      public EditProductObject()
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
            catch
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

      void OnIncreasePrice(object sender, RoutedEventArgs e)
      {
         product.UnitCost *= 1.1M;
      }
   }
}