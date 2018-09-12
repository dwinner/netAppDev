using System.Windows;

namespace WpfWindowsLibrary
{
	public partial class SimpleControl
	{
		public SimpleControl()
		{
			InitializeComponent();
		}

		private void OnClick(object sender, RoutedEventArgs e) => MessageBox.Show("Hello from WPF");
	}
}