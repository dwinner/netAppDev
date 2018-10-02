using Microsoft.Practices.Unity;
using MvvmPrimer.ViewModels;

namespace MvvmPrimer
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		[Dependency]
		public ApplicationViewModel ViewModel
		{
			set { DataContext = value; }
		}
	}
}