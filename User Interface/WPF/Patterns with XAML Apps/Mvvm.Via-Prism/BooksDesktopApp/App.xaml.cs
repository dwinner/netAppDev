using System;
using System.Windows;
using Contracts;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;
using Repositories;
using ViewModels;

namespace BooksDesktopApp
{
   public partial class App
   {
      public IServiceProvider Container { get; private set; }

      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);
         RegisterDiContainer();
         var mainWindow = new MainWindow();
         mainWindow.Show();
      }

      private void RegisterDiContainer()
      {
         var services = new ServiceCollection();
         services.AddTransient<BooksViewModel>();
         services.AddTransient<BookViewModel>();
         services.AddSingleton<IBooksRepository, BooksRepository>();
         services.AddSingleton<IEventAggregator, EventAggregator>();

         Container = services.BuildServiceProvider();
      }
   }
}