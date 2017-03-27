using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StoreDatabase;

namespace DataBinding
{
	public partial class ValidationTest
	{
		private ICollection<Product> _products;

		public ValidationTest()
		{
			InitializeComponent();
		}

		private void OnGetProducts(object sender, RoutedEventArgs e)
		{
			_products = App.StoreDb.GetProducts();
			LstProducts.ItemsSource = _products;
		}

		private void OnUpdateProduct(object sender, RoutedEventArgs e)
		{
			FocusManager.SetFocusedElement(this, (Button) sender);
		}

		private void OnValidationError(object sender, ValidationErrorEventArgs e)
		{
			if (e.Action == ValidationErrorEventAction.Added)
				MessageBox.Show(e.Error.ErrorContent.ToString());
		}

		private void OnGetExceptions(object sender, RoutedEventArgs e)
		{
			var builder = new StringBuilder();
			GetErrors(builder, GridProductDetails);
			var message = builder.ToString();
			if (message != string.Empty)
				MessageBox.Show(message);
		}

		private static void GetErrors(StringBuilder builder, DependencyObject obj)
		{
			foreach (var element in LogicalTreeHelper.GetChildren(obj).OfType<TextBox>())
			{
				if (Validation.GetHasError(element))
				{
					builder.AppendFormat("{0} has errors:{1}", element.Text, Environment.NewLine);
					foreach (var error in Validation.GetErrors(element))
						builder.AppendFormat("  {0}{1}", error.ErrorContent, Environment.NewLine);
				}

				// Check the children of this object.
				GetErrors(builder, element);
			}
		}
	}
}