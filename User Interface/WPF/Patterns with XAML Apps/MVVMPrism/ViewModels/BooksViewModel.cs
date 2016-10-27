using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Contracts;
using Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ViewModels.Events;

namespace ViewModels
{
   public class BooksViewModel : BindableBase
   {
      private readonly ObservableCollection<Book> _books = new ObservableCollection<Book>();
      private readonly IBooksRepository _booksRepository;
      private readonly IEventAggregator _eventAggregator;
      private bool _canGetBooks = true;
      private Book _selectedBook;

      public BooksViewModel(IBooksRepository booksRepository, IEventAggregator eventAggregator)
      {
         _booksRepository = booksRepository;
         _eventAggregator = eventAggregator;
         GetBooksCommand = new DelegateCommand(() => OnGetBooksAsync().ConfigureAwait(true), CanGetBooks);
         AddBookCommand = new DelegateCommand(OnAddBook);
      }

      public Book SelectedBook
      {
         get { return _selectedBook; }
         set
         {
            if (SetProperty(ref _selectedBook, value))
            {
               _eventAggregator.GetEvent<BookEvent>().Publish(new BookInfo {BookId = _selectedBook.BookId});
            }
         }
      }

      public IEnumerable<Book> Books => _books;
      public ICommand GetBooksCommand { get; }
      public ICommand AddBookCommand { get; }

      public async Task OnGetBooksAsync()
      {
         (GetBooksCommand as DelegateCommand)?.RaiseCanExecuteChanged();
         var books = await _booksRepository.GetItemsAsync().ConfigureAwait(true);
         _books.Clear();
         foreach (var book in books)
         {
            _books.Add(book);
         }

         _canGetBooks = true;
         (GetBooksCommand as DelegateCommand)?.RaiseCanExecuteChanged();
      }

      public bool CanGetBooks() => _canGetBooks;

      private void OnAddBook()
         => _eventAggregator.GetEvent<BookEvent>().Publish(new BookInfo {BookId = 0});
   }
}