using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace HostingWinFormsViaCode
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			// Create the host and the PropertyGrid control
			var propertyGrid = new PropertyGrid();
			var winFormsHost = new WindowsFormsHost {Child = propertyGrid};

			// Add the PropertyGrid to the host, and the host to the WPF Grid
			TopLevelGrid.Children.Add(winFormsHost);
			propertyGrid.SelectedObject = this;
		}
	}
}