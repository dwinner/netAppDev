using AppDev.Sapper.AppLoader.ViewModels;
using JetBrains.Annotations;
using Microsoft.Practices.Unity;

namespace AppDev.Sapper.AppLoader
{
	public partial class SapperWindow
	{
		public SapperWindow()
		{
			InitializeComponent();
		}

		[NotNull]
		[Dependency]
		[UsedImplicitly]
		public SapperWindowViewModel ViewModel
		{
			set { DataContext = value; }
		}
	}
}