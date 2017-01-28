using System.Collections.Generic;
using System.Windows;
using StoreDatabase;

namespace DataBinding
{
	public partial class DataTemplateByType
	{
		private ICollection<Product> _products;

		public DataTemplateByType()
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