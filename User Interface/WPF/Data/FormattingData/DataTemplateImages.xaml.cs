using System.Windows;

namespace DataBinding
{
	public partial class DataTemplateImages
	{
		public DataTemplateImages()
		{
			InitializeComponent();
		}

		private void OnGetProducts(object sender, RoutedEventArgs e)
			=> ProductListBox.ItemsSource = App.StoreDb.GetProducts();
	}
}