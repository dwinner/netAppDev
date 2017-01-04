using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace DataBinding
{
	public partial class DataTemplateControls
	{
		public DataTemplateControls()
		{
			InitializeComponent();
			CategoryListBox.ItemsSource = App.StoreDbDataSet.GetCategoriesAndProducts().Tables["Categories"].DefaultView;
		}

		private void OnView(object sender, RoutedEventArgs e)
		{
			var button = (Button) sender;
			var row = (DataRowView) button.Tag;
			CategoryListBox.SelectedItem = row;

			// Alternate selection approach.
			//ListBoxItem item = (ListBoxItem)lstCategories.ItemContainerGenerator.ContainerFromItem(row);
			//item.IsSelected = true;

			MessageBox.Show($"You chose category #{row["CategoryID"]}: {(string) row["CategoryName"]}");
		}
	}
}