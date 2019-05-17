using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Ninject.Extensions.Factory;
using Northwind.Wpf.Infrastructure;
using Northwind.Wpf.Views;

namespace Northwind.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var kernel = new StandardKernel(new ServiceRegistration());

            var viewFactory = kernel.Get<IViewFactory>();
            var mainWindow = viewFactory.CreateView<IMainView>();

            mainWindow.Show();
        }
    }
}
