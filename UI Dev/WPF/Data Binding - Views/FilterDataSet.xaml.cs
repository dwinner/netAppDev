using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Data;
using StoreDatabase;

namespace DataBinding
{
	public partial class FilterDataSet
	{
		private DataTable _products;

		public FilterDataSet()
		{
			InitializeComponent();
		}

		private void OnGetProducts(object sender, RoutedEventArgs e)
		{
			_products = App.StoreDbDataSet.GetProducts();
			ProductListBox.ItemsSource = _products.DefaultView;
			var view = CollectionViewSource.GetDefaultView(ProductListBox.ItemsSource);
			view.SortDescriptions.Add(new SortDescription(nameof(Product.ModelName), ListSortDirection.Ascending));
		}

		private void OnApplyFilter(object sender, RoutedEventArgs e)
		{
			decimal minimumPrice;
			if (decimal.TryParse(MinPriceTextBox.Text, out minimumPrice))
			{
				var view = CollectionViewSource.GetDefaultView(ProductListBox.ItemsSource) as BindingListCollectionView;
				if (view != null)
				{
					view.CustomFilter = $"{nameof(Product.UnitCost)} > {minimumPrice}";
				}
			}
		}

		private void OnRemoveFilter(object sender, RoutedEventArgs e)
		{
			var view = CollectionViewSource.GetDefaultView(ProductListBox.ItemsSource) as BindingListCollectionView;
			if (view != null)
			{
				view.CustomFilter = string.Empty;
			}
		}
	}
}