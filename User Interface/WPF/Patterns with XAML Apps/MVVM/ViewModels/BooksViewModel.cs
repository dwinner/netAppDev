using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Contracts;
using Contracts.Events;
using Framework;
using Models;

namespace ViewModels
{
   public class BooksViewModel : ViewModelBase
   {
      private readonly IBooksService _booksService;
      private bool _canGetBooks = true;
      private Book _selectedBook;

      public BooksViewModel(IBooksService booksService)
      {
         _booksService = booksService;
         GetBooksCommand = new DelegateCommand(
            () => OnGetBooksAsync().ConfigureAwait(true), CanGetBooks);
         AddBookCommand = new DelegateCommand(OnAddBook);
      }

      public Book SelectedBook
      {
         get { return _selectedBook; }
         set
         {
            if (SetProperty(ref _selectedBook, value))
            {
               EventAggregator<BookInfoEvent>.Instance.Publish(
                  this, new BookInfoEvent {BookId = _selectedBook.BookId});
            }
         }
      }

      public IEnumerable<Book> Books => _booksService.Books;

      public ICommand GetBooksCommand { get; }
      public ICommand AddBookCommand { get; }

      private async Task OnGetBooksAsync()
      {
         await _booksService.LoadBooksAsync().ConfigureAwait(true);
         _canGetBooks = false;
         (GetBooksCommand as DelegateCommand)?.RaiseCanExecuteChanged();
      }

      private bool CanGetBooks() => _canGetBooks;

      private void OnAddBook() =>
         EventAggregator<BookInfoEvent>.Instance.Publish(this, new BookInfoEvent {BookId = 0});
   }
}