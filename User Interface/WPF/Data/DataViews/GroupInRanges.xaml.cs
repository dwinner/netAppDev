using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using StoreDatabase;

namespace DataBinding
{
	public partial class GroupInRanges
	{
		private ICollection<Product> _products;

		public GroupInRanges()
		{
			InitializeComponent();
		}

		private void OnGetProducts(object sender, RoutedEventArgs e)
		{
			_products = App.StoreDb.GetProducts();
			var viewSource = (CollectionViewSource) FindResource("GroupByRangeView");
			viewSource.Source = _products;
			//lstProducts.ItemsSource = _products;

			//ICollectionView view = CollectionViewSource.GetDefaultView(lstProducts.ItemsSource);
			//view.SortDescriptions.Add(new SortDescription("UnitCost", ListSortDirection.Ascending));
			//PriceRangeProductGrouper grouper = new PriceRangeProductGrouper();
			//grouper.GroupInterval = 50;
			//view.GroupDescriptions.Add(new PropertyGroupDescription("UnitCost", grouper));
		}
	}
}