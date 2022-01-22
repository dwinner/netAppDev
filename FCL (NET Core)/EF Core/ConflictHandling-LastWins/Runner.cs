using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class Runner
{
   private readonly BooksContext _booksContext;
   private Book? _selectedBook;
   private string? _user;

   public Runner(BooksContext booksContext) => _booksContext = booksContext;

   public async Task CreateTheDatabaseAsync()
   {
      var created = await _booksContext.Database.EnsureCreatedAsync();
      string creationInfo = created ? "created" : "exists";
      Console.WriteLine($"database {creationInfo}");
   }

   public async Task DeleteDatabaseAsync()
   {
      Console.Write("Delete the database? (y|n) ");
      var input = Console.ReadLine();
      if (input?.ToLower() == "y")
      {
         var deleted = await _booksContext.Database.EnsureDeletedAsync();
         string deletionInfo = deleted ? "deleted" : "not deleted";
         Console.WriteLine($"database {deletionInfo}");
      }
   }

   public async Task<int> PrepareUpdateAsync(string user, int id = 0)
   {
      _user = user;
      if (id is 0)
      {
         _selectedBook = await _booksContext.Books.OrderBy(b => b.BookId).LastAsync();
         return _selectedBook.BookId;
      }

      _selectedBook = await _booksContext.Books.FindAsync(id);
      return id;
   }

   public async Task UpdateAsync()
   {
      if (_selectedBook is null)
      {
         throw new InvalidOperationException("_selectedBook not set. Invoke PrepareUpdateAsync before UpdateAsync");
      }

      _selectedBook.Title = $"Book updated from {_user}";
      var records = await _booksContext.SaveChangesAsync();
      if (records == 1)
      {
         Console.WriteLine($"Book {_selectedBook.BookId} updated from {_user}");
      }
   }

   public async Task<string> GetUpdatedTitleAsyc(int id)
   {
      var book = await _booksContext.Books.FindAsync(id);
      return $"{book.Title} with id {book.BookId}";
   }
}