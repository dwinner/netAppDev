using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StoreDatabase;

namespace DataBinding
{
	public partial class ValueConverter
	{
		private Product _product;

		public ValueConverter()
		{
			InitializeComponent();
		}

		private void OnGetProduct(object sender, RoutedEventArgs e)
		{
			int id;
			if (int.TryParse(IdTextBox.Text, out id))
			{
				try
				{
					_product = App.StoreDb.GetProduct(id);
					ProductDetailsGrid.DataContext = _product;
				}
				catch (Exception)
				{
					MessageBox.Show("Error contacting database.");
				}
			}
			else
			{
				MessageBox.Show("Invalid ID.");
			}
		}

		private void OnUpdateProduct(object sender, RoutedEventArgs e)
		{
			// Make sure update has taken place.
			FocusManager.SetFocusedElement(this, (Button) sender);
			MessageBox.Show(_product.UnitCost.ToString(CultureInfo.InvariantCulture));
		}
	}
}