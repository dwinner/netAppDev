using System.Linq;
using System.Windows;
using StoreDatabase;

namespace DataBinding
{
	public partial class CheckBoxList
	{
		public CheckBoxList()
		{
			InitializeComponent();
			ProductsListBox.ItemsSource = App.StoreDb.GetProducts();
		}

		private void OnGetSelectedItems(object sender, RoutedEventArgs e)
		{
			if (ProductsListBox.SelectedItems.Count > 0)
			{
				var items = ProductsListBox.SelectedItems.Cast<Product>()
					.Aggregate("You selected: ", (current, product) => $"{current}\n  * {product.ModelName}");
				MessageBox.Show(items);
			}
		}
	}
}