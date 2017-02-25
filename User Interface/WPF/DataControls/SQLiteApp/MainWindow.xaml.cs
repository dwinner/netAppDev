using Microsoft.Practices.Unity;
using ReactiveUI;
using SQLiteApp.ViewModels;

namespace SQLiteApp
{
	public partial class MainWindow : IViewFor<MainWindowViewModel>
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = (MainWindowViewModel) value; }
		}

		[Dependency]
		public MainWindowViewModel ViewModel { get; set; }		
	}
}