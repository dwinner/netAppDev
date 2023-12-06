using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BooksLib.Models;
using BooksLib.Repositories;

namespace BooksLib.Services;

public class BooksService(IBooksRepository repository) : IBooksService
{
   private readonly ObservableCollection<Book> _books = new();

   public async Task LoadBooksAsync()
   {
      if (_books.Count > 0)
      {
         return;
      }

      var books = await repository.GetItemsAsync()
         .ConfigureAwait(false);
      _books.Clear();
      foreach (var b in books)
      {
         _books.Add(b);
      }
   }

   public Book? GetBook(int bookId) =>
      _books.SingleOrDefault(b => b.BookId == bookId);

   public async Task<Book> AddOrUpdateBookAsync(Book book)
   {
      if (book is null)
      {
         throw new ArgumentNullException(nameof(book));
      }

      Book? updated;
      if (book.BookId == 0)
      {
         updated = await repository.AddAsync(book)
            .ConfigureAwait(false);
         _books.Add(updated);
      }
      else
      {
         updated = await repository.UpdateAsync(book)
            .ConfigureAwait(false);
         if (updated is null)
         {
            throw new InvalidOperationException();
         }

         var old = _books.Single(b => b.BookId == updated.BookId);
         var ix = _books.IndexOf(old);
         _books.RemoveAt(ix);
         _books.Insert(ix, updated);
      }

      return updated;
   }

   public IEnumerable<Book> Books => _books;
}