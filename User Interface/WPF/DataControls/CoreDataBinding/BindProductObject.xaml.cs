using System.Windows;

namespace DataBinding
{   
   public partial class BindProductObject
   {
      public BindProductObject()
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
               gridProductDetails.DataContext = App.StoreDb.GetProduct(id);
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
   }
}