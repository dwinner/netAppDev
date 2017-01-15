namespace DataBinding
{
   public partial class BoundTreeViewDataSet
   {
      public BoundTreeViewDataSet()
      {
         InitializeComponent();
         var categoriesAndProducts = App.StoreDbDataSet.GetCategoriesAndProducts();
         CategoryTreeView.ItemsSource = categoriesAndProducts.Tables["Categories"].DefaultView;
      }
   }
}