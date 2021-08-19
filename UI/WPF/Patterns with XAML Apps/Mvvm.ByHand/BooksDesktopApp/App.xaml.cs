using System;
using System.Windows;
using Contracts;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;
using ViewModels;

namespace BooksDesktopApp
{
   public partial class App
   {
      private BooksService _booksService;

      private IServiceProvider Container { get; set; }

      public BooksService BooksService =>
         _booksService ?? (_booksService = new BooksService(new BooksSampleRepository()));

      private static IServiceProvider RegisterServices()
      {
         var serviceCollection = new ServiceCollection();
         serviceCollection.AddTransient<BooksViewModel>();
         serviceCollection.AddTransient<BookViewModel>();
         serviceCollection.AddSingleton<IBooksService, BooksService>();
         //serviceCollection.AddSingleton<IBooksRepository, BooksSampleRepository>();
         return serviceCollection.BuildServiceProvider();
      }

      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);
         Container = RegisterServices();
         var mainWindow = new MainWindow();
         mainWindow.Show();
      }
   }
}