using SQLiteApp.ViewModels;

namespace SQLiteApp
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new AppViewModel();
		}
	}
}