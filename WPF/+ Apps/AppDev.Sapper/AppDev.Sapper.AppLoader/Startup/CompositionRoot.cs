using AppDev.Sapper.AppLoader.Infrastructure;
using Microsoft.Practices.Unity;

namespace AppDev.Sapper.AppLoader.Startup
{
   /// <summary>
   ///    IoC composition root
   /// </summary>
   internal static class CompositionRoot
   {
      internal static void SetupDependencies()
      {
         IUnityContainer container = new UnityContainer();
         container.RegisterType<IGameFieldFactory, DefaultGameFieldFactory>();
         var sapperWindow = container.Resolve<SapperWindow>();
         sapperWindow.Show();
      }
   }
}