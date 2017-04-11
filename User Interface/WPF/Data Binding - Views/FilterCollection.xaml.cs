using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using StoreDatabase;

namespace DataBinding
{
	public partial class FilterCollection
	{
		private ProductByPriceFilterer _filterer;
		private ICollection<Product> _products;

		public FilterCollection()
		{
			InitializeComponent();
		}

		private void OnGetProducts(object sender, RoutedEventArgs e)
		{
			_products = App.StoreDb.GetProducts();
			ProductListBox.ItemsSource = _products;

			var productsView = CollectionViewSource.GetDefaultView(ProductListBox.ItemsSource);
			productsView.SortDescriptions.Add(new SortDescription(nameof(Product.ModelName), ListSortDirection.Ascending));

			var listCollectionView = CollectionViewSource.GetDefaultView(ProductListBox.ItemsSource) as ListCollectionView;			
			// Now if you edit and reduce the price (below the filter condition) the record will disappear automatically.
			if (listCollectionView != null)
			{
				listCollectionView.IsLiveFiltering = true;
				listCollectionView.LiveFilteringProperties.Add(nameof(Product.UnitCost));
			}

			//view.GroupDescriptions.Add(new PropertyGroupDescription("CategoryName"));
			//ListCollectionView view = (ListCollectionView)CollectionViewSource.GetDefaultView(lstProducts.ItemsSource);
			//view.CustomSort = new SortByModelNameLength();
		}

		private void OnApplyFilter(object sender, RoutedEventArgs e)
		{
			decimal minimumPrice;
			if (decimal.TryParse(MinPriceTextBox.Text, out minimumPrice))
			{
				var listCollectionView = CollectionViewSource.GetDefaultView(ProductListBox.ItemsSource) as ListCollectionView;
				if (listCollectionView != null)
				{
					_filterer = new ProductByPriceFilterer(minimumPrice);
					listCollectionView.Filter = _filterer.FilterItem;
					listCollectionView.Refresh();
				}
			}
		}

		private void OnRemoveFilter(object sender, RoutedEventArgs e)
		{
			var listCollectionView = CollectionViewSource.GetDefaultView(ProductListBox.ItemsSource) as ListCollectionView;
			if (listCollectionView != null)
			{
				listCollectionView.Filter = null;
			}
		}

		private void OnMinPriceChanged(object sender, TextChangedEventArgs e)
		{
			var view = CollectionViewSource.GetDefaultView(ProductListBox.ItemsSource) as ListCollectionView;
			if (view != null)
			{
				decimal minimumPrice;
				if (decimal.TryParse(MinPriceTextBox.Text, out minimumPrice) && (_filterer != null))
				{
					_filterer.MinimumPrice = minimumPrice;
					//view.Refresh();
				}
			}
		}
	}

/*
	public class SortByModelNameLength : IComparer
	{
		public int Compare(object x, object y)
		{
			var productX = (Product) x;
			var productY = (Product) y;
			return productX.ModelName.Length.CompareTo(productY.ModelName.Length);
		}
	}
*/
}