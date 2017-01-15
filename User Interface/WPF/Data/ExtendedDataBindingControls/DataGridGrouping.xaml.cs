using System.Windows.Data;
using StoreDatabase;

namespace DataBinding
{
   public partial class DataGridGrouping
   {
      public DataGridGrouping()
      {
         InitializeComponent();

         ProductDataGrid.ItemsSource = App.StoreDb.GetProducts();
         var view = CollectionViewSource.GetDefaultView(ProductDataGrid.ItemsSource);
         view.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Product.CategoryName)));
      }
   }
}