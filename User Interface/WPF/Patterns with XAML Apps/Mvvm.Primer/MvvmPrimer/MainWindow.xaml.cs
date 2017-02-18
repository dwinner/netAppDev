using MvvmPrimer.ViewModels;

namespace MvvmPrimer
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new ApplicationViewModel();
		}
	}
}