using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Models;

namespace Services
{
   public class BooksService : IBooksService
   {
      private readonly ObservableCollection<Book> _books = new ObservableCollection<Book>();
      private readonly IBooksRepository _booksRepository;

      public BooksService(IBooksRepository repository)
      {
         _booksRepository = repository;
      }

      public async Task LoadBooksAsync()
      {
         if (_books.Count > 0)
         {
            return;
         }

         _books.Clear();
         var books = await _booksRepository.GetItemsAsync().ConfigureAwait(true);
         foreach (var book in books)
         {
            _books.Add(book);
         }
      }

      public Book GetBook(int bookId) => _books.SingleOrDefault(book => book.BookId == bookId);

      public async Task<Book> AddOrUpdateBookAsync(Book book)
      {
         Book updated;
         if (book.BookId == 0)
         {
            updated = await _booksRepository.AddAsync(book).ConfigureAwait(true);
            _books.Add(updated);
         }
         else
         {
            updated = await _booksRepository.UpdateAsync(book).ConfigureAwait(true);
            var old = _books.Single(bk => bk.BookId == updated.BookId);
            var ix = _books.IndexOf(old);
            _books.RemoveAt(ix);
            _books.Insert(ix, updated);
         }

         return updated;
      }

      IEnumerable<Book> IBooksService.Books => _books;
   }
}