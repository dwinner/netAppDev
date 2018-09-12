using System.Windows;
using static AppDev.Sapper.AppLoader.Startup.AutomapperConfig;
using static AppDev.Sapper.AppLoader.Startup.CompositionRoot;

namespace AppDev.Sapper.AppLoader
{
   public partial class App
   {
      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         ConfigureViewModelMapping();
         SetupDependencies();
      }
   }
}