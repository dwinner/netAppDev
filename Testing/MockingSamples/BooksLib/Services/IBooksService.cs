using System.Collections.Generic;
using System.Threading.Tasks;
using BooksLib.Models;

namespace BooksLib.Services;

public interface IBooksService
{
   IEnumerable<Book> Books { get; }
   Task<Book> AddOrUpdateBookAsync(Book book);
   Book? GetBook(int bookId);
   Task LoadBooksAsync();
}