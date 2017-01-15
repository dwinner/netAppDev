using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using StoreDatabase;

namespace DataBinding
{
	public partial class GroupList
	{
		private ICollection<Product> _products;

		public GroupList()
		{
			InitializeComponent();
		}

		private void OnGetProducts(object sender, RoutedEventArgs e)
		{
			_products = App.StoreDb.GetProducts();
			ProductListBox.ItemsSource = _products;

			var view = CollectionViewSource.GetDefaultView(ProductListBox.ItemsSource);
			const string categoryPropertyName = nameof(Product.CategoryName);
			view.SortDescriptions.Add(new SortDescription(categoryPropertyName, ListSortDirection.Ascending));
			view.SortDescriptions.Add(new SortDescription(nameof(Product.ModelName), ListSortDirection.Ascending));

			view.GroupDescriptions.Add(new PropertyGroupDescription(categoryPropertyName));
		}
	}
}