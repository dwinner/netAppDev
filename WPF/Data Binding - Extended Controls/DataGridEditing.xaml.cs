namespace DataBinding
{
   public partial class DataGridEditing
   {
      public DataGridEditing()
      {
         InitializeComponent();
         CategoryColumn.ItemsSource = App.StoreDb.GetCategories();
         ProductDataGrid.ItemsSource = App.StoreDb.GetProducts();
      }
   }
}