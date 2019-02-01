using System.Windows;
using Microsoft.Practices.Unity;
using MvvmPrimer.Services;

namespace MvvmPrimer
{
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			IUnityContainer container = new UnityContainer();
			container.RegisterType<IFileService, JsonFileService>();
			container.RegisterType<IDialogService, DefaultDialogService>();
			var mainWindow = container.Resolve<MainWindow>();
			mainWindow.Show();
		}
	}
}