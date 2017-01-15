namespace DataBinding
{
   public partial class BoundTreeView
   {
      public BoundTreeView()
      {
         InitializeComponent();
         CategoryTreeView.ItemsSource = App.StoreDb.GetCategoriesAndProducts();
      }
   }
}