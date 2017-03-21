using Microsoft.Practices.Unity;
using SQLiteApp.ViewModels;

namespace SQLiteApp
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		[Dependency]
		public MainWindowViewModel ViewModel
		{
			set { DataContext = value; }
		}
	}
}