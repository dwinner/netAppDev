using System.Windows;

namespace DataBinding
{
	public partial class RadioButtonList
	{
		public RadioButtonList()
		{
			InitializeComponent();
			ProductsListBox.ItemsSource = App.StoreDb.GetProducts();
		}

		private void OnGetSelectedItem(object sender, RoutedEventArgs e)
			=> MessageBox.Show(ProductsListBox.SelectedItem.ToString());
	}
}