using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using StoreDatabase;

namespace DataBinding
{
	public partial class VariedStyles
	{
		private ICollection<Product> _products;

		public VariedStyles()
		{
			InitializeComponent();
		}

		private void OnGetProducts(object sender, RoutedEventArgs e)
		{
			_products = App.StoreDb.GetProducts();
			ProductsListBox.ItemsSource = _products;
		}

		private void OnApplyChange(object sender, RoutedEventArgs e)
		{
			((ObservableCollection<Product>) _products)[1].CategoryName = "Travel";
			var selector = ProductsListBox.ItemContainerStyleSelector;
			ProductsListBox.ItemContainerStyleSelector = null;
			ProductsListBox.ItemContainerStyleSelector = selector;
		}
	}
}