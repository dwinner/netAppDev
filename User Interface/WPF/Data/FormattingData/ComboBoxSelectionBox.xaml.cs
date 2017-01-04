using System.Windows;

namespace DataBinding
{
	public partial class ComboBoxSelectionBox
	{
		public ComboBoxSelectionBox()
		{
			InitializeComponent();
			ProductComboBox.ItemsSource = App.StoreDb.GetProducts();
			// Select the first item.
			ProductComboBox.SelectedIndex = 0;
		}

		private void OnTextChanged(object sender, RoutedEventArgs e)
		{
			// Re-select the so the new TextSearch.TextPath is evaluated.
			var currentIndex = ProductComboBox.SelectedIndex;
			ProductComboBox.SelectedIndex = -1;
			ProductComboBox.SelectedIndex = currentIndex;
		}
	}
}