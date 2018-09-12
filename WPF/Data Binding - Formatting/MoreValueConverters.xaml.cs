using System.Collections.Generic;
using System.Windows;
using StoreDatabase;

namespace DataBinding
{
	public partial class MoreValueConverters
	{
		private ICollection<Product> _products;

		public MoreValueConverters()
		{
			InitializeComponent();
		}

		private void OnGetProducts(object sender, RoutedEventArgs e)
		{
			_products = App.StoreDb.GetProducts();
			ProductsListBox.ItemsSource = _products;
		}
	}
}