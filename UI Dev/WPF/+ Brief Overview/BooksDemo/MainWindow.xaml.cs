using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BooksDemo.Data;
using BooksDemo.UserControls;

namespace BooksDemo
{
   public partial class MainWindow
   {
      private readonly ObservableCollection<UiControlInfo> _userControls = new ObservableCollection<UiControlInfo>();

      public MainWindow()
      {
         InitializeComponent();
         DataContext = this;
      }

      public IEnumerable<UiControlInfo> Controls => _userControls;

      private void OnAppClose(object sender, ExecutedRoutedEventArgs e) => Application.Current.Shutdown();

      private void OnShowBook(object sender, ExecutedRoutedEventArgs e)
         =>
            _userControls.Add(new UiControlInfo
            {
               Title = "Book",
               Content =
                  new BookUserControl
                  {
                     DataContext =
                        new Book
                        {
                           Title = "Professional C# 4 and .NET 4",
                           Publisher = "Wrox Press",
                           Isbn = "978-0-470-50225-9"
                        }
                  }
            });

      private void OnShowBooksList(object sender, ExecutedRoutedEventArgs e)
         => _userControls.Add(new UiControlInfo {Title = "Books List", Content = new BookListUserControl()});

      private void OnShowBooksGrid(object sender, ExecutedRoutedEventArgs e)
         => _userControls.Add(new UiControlInfo {Title = "Books Grid", Content = new BookGridUserControl()});
   }
}