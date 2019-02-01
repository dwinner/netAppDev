using System.Windows;

namespace DataBinding
{
	public partial class WrappedList
	{
		public WrappedList()
		{
			InitializeComponent();
		}

		private void OnGetProducts(object sender, RoutedEventArgs e)
			=> ProductListBox.ItemsSource = App.StoreDb.GetProducts();
	}
}