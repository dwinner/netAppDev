using System.Windows.Controls;
using System.Windows.Media;
using StoreDatabase;

namespace DataBinding
{   
   public partial class DataGridTest
   {
      // Reuse brush objects for efficiency in large data displays.
      private readonly SolidColorBrush _highlightBrush = new SolidColorBrush(Colors.Orange);
      private readonly SolidColorBrush _normalBrush = new SolidColorBrush(Colors.White);

      public DataGridTest()
      {
         InitializeComponent();
         ProductDataGrid.ItemsSource = App.StoreDb.GetProducts();
      }

      private void OnLoadingRow(object sender, DataGridRowEventArgs e)
      {
         var product = (Product) e.Row.DataContext;
         e.Row.Background = product.UnitCost > 100 ? _highlightBrush : _normalBrush;
      }

/*
      private void FormatRow(DataGridRow row)
      {
         var product = (Product) row.DataContext;
         row.Background = product.UnitCost > 100 ? _highlightBrush : _normalBrush;
      }
*/
   }
}