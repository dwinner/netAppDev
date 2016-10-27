using System.Windows;
using ViewModels;

namespace BooksDesktopApp.Views
{
   public partial class BooksView
   {
      public BooksView()
      {
         InitializeComponent();
      }

      public BooksViewModel ViewModel { get; } = new BooksViewModel((Application.Current as App)?.BooksService);
      // public BooksViewModel ViewModel { get; } = (App.Current as App).Container.GetService<BooksViewModel>();
   }
}