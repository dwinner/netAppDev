using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Contracts
{
   public interface IBooksService
   {
      IEnumerable<Book> Books { get; }
      Task LoadBooksAsync();
      Book GetBook(int bookId);
      Task<Book> AddOrUpdateBookAsync(Book book);
   }
}