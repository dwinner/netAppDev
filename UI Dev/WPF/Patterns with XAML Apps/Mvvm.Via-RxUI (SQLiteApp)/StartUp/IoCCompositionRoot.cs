using Microsoft.Practices.Unity;
using SQLiteApp.Infrastructure;

namespace SQLiteApp.StartUp
{
	internal static class IoCCompositionRoot
	{
		internal static void SetupDependencies()
		{
			IUnityContainer container = new UnityContainer();
			container.RegisterType<IPhoneStoreRepository, SqlitePhoneStoreRepository>();
			var mainWindow = container.Resolve<MainWindow>();
			mainWindow.Show();
		}
	}
}