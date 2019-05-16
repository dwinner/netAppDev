using System.Windows;
using Ninject;
using Ninject.Extensions.Conventions;
using Northwind.Wpf.Infrastructure;

namespace Northwind.Wpf
{
   public partial class App
   {
      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         using (var kernel = new StandardKernel())
         {
            kernel.Bind(x => x.FromAssembliesMatching("Northwind.*")
               .SelectAllClasses()
               .BindAllInterfaces());

            kernel.Bind(x => x.FromThisAssembly()
               .SelectAllInterfaces()
               .EndingWith("Factory")
               .BindToFactory()
               .Configure(c => c.InSingletonScope()));

            var mainWindow = kernel.Get<IMainView>();
            mainWindow.Show();
         }
      }
   }
}