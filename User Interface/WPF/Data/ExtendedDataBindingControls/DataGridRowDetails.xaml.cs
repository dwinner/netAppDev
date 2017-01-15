namespace DataBinding
{
   public partial class DataGridRowDetails
   {
      public DataGridRowDetails()
      {
         InitializeComponent();
         ProductDataGrid.ItemsSource = App.StoreDb.GetProducts();         
      }
   }
}