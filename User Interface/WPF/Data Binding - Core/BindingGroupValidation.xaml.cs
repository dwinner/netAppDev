using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StoreDatabase;

namespace DataBinding
{
	public partial class BindingGroupValidation
	{
		private ICollection<Product> _products;

		public BindingGroupValidation()
		{
			InitializeComponent();
		}

		private void OnGetProducts(object sender, RoutedEventArgs e)
		{
			_products = App.StoreDb.GetProducts();
			ProductsListBox.ItemsSource = _products;
		}

		private void OnUpdateProduct(object sender, RoutedEventArgs e)
		{
			// Make sure update has taken place.
			FocusManager.SetFocusedElement(this, (Button) sender);
		}

		private void OnLostFocus(object sender, RoutedEventArgs e)
		{
			ProductBindingGroup.CommitEdit();
		}

		private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ProductBindingGroup.CommitEdit();
		}
	}
}