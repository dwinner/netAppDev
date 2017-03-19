using System.Windows;
using static SQLiteApp.StartUp.AutoMapperConfig;
using static SQLiteApp.StartUp.IoCCompositionRoot;

namespace SQLiteApp
{
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			ConfigureViewModelMapping();
			SetupDependencies();
		}
	}
}