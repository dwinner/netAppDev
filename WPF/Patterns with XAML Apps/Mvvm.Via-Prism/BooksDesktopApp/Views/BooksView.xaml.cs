using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ViewModels;

namespace BooksDesktopApp.Views
{
   public partial class BooksView
   {
      public BooksView()
      {
         InitializeComponent();
         var vm = (Application.Current as App)?.Container.GetService<BooksViewModel>();
         DataContext = vm; // new BooksViewModel(new BooksRepository());
      }
   }
}