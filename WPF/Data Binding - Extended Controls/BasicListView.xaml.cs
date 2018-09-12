namespace DataBinding
{
   public partial class BasicListView
   {
      public BasicListView()
      {
         InitializeComponent();
         ProductListView.ItemsSource = App.StoreDb.GetProducts();
      }
   }
}