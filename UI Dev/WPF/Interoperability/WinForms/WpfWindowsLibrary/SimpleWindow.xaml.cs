using System.Windows;

namespace WpfWindowsLibrary
{
	public partial class SimpleWindow
	{
		public SimpleWindow()
		{
			InitializeComponent();
		}

		private void OnClick(object sender, RoutedEventArgs e) => MessageBox.Show("Hello from WPF");
	}
}