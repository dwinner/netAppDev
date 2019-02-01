using System.Collections.Generic;
using System.Windows;
using StoreDatabase;

namespace DataBinding
{
	public partial class DataTemplateList
	{
		private ICollection<Product> _products;

		public DataTemplateList()
		{
			InitializeComponent();
		}

		private void OnGetProducts(object sender, RoutedEventArgs e)
		{
			_products = App.StoreDb.GetProducts();
			ProductListBox.ItemsSource = _products;
		}
	}
}