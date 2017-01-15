using System.Windows;
using System.Windows.Controls;

namespace DataBinding
{
	public partial class Menu
	{
		public Menu()
		{
			InitializeComponent();
		}

		private void ButtonClick(object sender, RoutedEventArgs e)
		{
			// Get the current button.
			var source = (Button) e.OriginalSource;

			// Create an instance of the window named
			// by the current button.
			var type = GetType();
			var assembly = type.Assembly;
			var window = (Window) assembly.CreateInstance($"{type.Namespace}.{source.Content}");

			// Show the window.
			window?.ShowDialog();
		}
	}
}