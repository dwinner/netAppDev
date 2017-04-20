using System.Windows;
using ViewModels;

namespace BooksDesktopApp.Views
{
   public partial class BookView
   {
      public BookView()
      {
         InitializeComponent();
      }

      public BookViewModel ViewModel { get; }
         = new BookViewModel((Application.Current as App)?.BooksService);
   }
}