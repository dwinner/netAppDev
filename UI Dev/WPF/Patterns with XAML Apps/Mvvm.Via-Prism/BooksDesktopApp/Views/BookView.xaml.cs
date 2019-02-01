using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ViewModels;

namespace BooksDesktopApp.Views
{
   public partial class BookView
   {
      public BookView()
      {
         InitializeComponent();
         var vm = (Application.Current as App)?.Container.GetService<BookViewModel>();
         DataContext = vm; // new BooksViewModel(new BooksRepository());
      }
   }
}