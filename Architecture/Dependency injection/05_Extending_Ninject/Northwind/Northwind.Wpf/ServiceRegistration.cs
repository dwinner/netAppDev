using System.Windows.Input;
using Infrastructure;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Northwind.Domain;
using Northwind.SqlDataAccess;
using Northwind.Wpf.Infrastructure;
using Northwind.Wpf.Views;

namespace Northwind.Wpf
{
    public class ServiceRegistration : NinjectModule
    {
        public override void Load()
        {

            Bind<ApplicationSettings>().ToSelf().InSingletonScope();

            var applicationSettings = Kernel.Get<ApplicationSettings>();

            var connectionString = applicationSettings
                .GetConnectionSetring("NorthwindConnectionString").ConnectionString;

            Bind<ICustomerRepository>().To<SqlCustomerRepository>()
                .WithConstructorArgument("connectionString", connectionString);

            Bind<IViewFactory>().ToFactory().InSingletonScope();
            Bind<IMainView>().To<MainWindow>();
            Bind<ICustomerView>().To<CustomerWindow>();

            Bind<ICommand>().To<ActionCommand>();
            Bind<ICommandFactory>().ToFactory().InSingletonScope();

        }


    }
}