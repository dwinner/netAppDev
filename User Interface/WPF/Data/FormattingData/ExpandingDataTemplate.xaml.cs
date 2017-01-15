namespace DataBinding
{
	public partial class ExpandingDataTemplate
	{
		public ExpandingDataTemplate()
		{
			InitializeComponent();
			CategoryListBox.ItemsSource = App.StoreDb.GetProducts();
		}
	}
}