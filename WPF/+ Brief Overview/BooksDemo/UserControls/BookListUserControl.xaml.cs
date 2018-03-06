using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using BooksDemo.Data;

namespace BooksDemo.UserControls
{
   public partial class BookListUserControl
   {
      public BookListUserControl()
      {
         InitializeComponent();
      }

      private void OnAddBook(object sender, RoutedEventArgs e)
         =>
            ((TryFindResource("BookListProvider") as ObjectDataProvider)?.Data as IList<Book>)?.Add(
               new Book("HTML and CSS: Design and Build Websites", "Wiley", "978-1118-00818-8", "John Ducket"));
   }
}