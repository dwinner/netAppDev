using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _06_BooksSample
{
   public class QuerySamples
   {
      public static async Task QueryAllBooksAsync()
      {
         Console.WriteLine(nameof(QueryAllBooksAsync));

         await using var context = new BooksContext();
         var books = await context.Books.ToListAsync().ConfigureAwait(false);
         foreach (var b in books)
         {
            Console.WriteLine(b);
         }

         Console.WriteLine();
      }

      public static async Task QueryAllBooksWithAsyncEnumerableAsync()
      {
         Console.WriteLine(nameof(QueryAllBooksWithAsyncEnumerableAsync));

         await using var context = new BooksContext();
         await foreach (var currentBook in context.Books.AsAsyncEnumerable())
         {
            Console.WriteLine(currentBook);
         }

         Console.WriteLine();
      }

      public static async Task QueryBookByKeyAsync(int aBookId)
      {
         Console.WriteLine(nameof(QueryBookByKeyAsync));

         await using var context = new BooksContext();
         var book = await context.Books.FindAsync(aBookId).ConfigureAwait(false);
         if (book != null)
         {
            Console.WriteLine($"found book {book}");
         }

         Console.WriteLine();
      }

      public static async Task QueryBooksAsync()
      {
         await using var context = new BooksContext();
         var wroxBooks = await context.Books
            .Where(b => b.Publisher == "Wrox Press")
            .ToListAsync().ConfigureAwait(false);

         foreach (var currentBook in wroxBooks)
         {
            Console.WriteLine($"{currentBook.Title} {currentBook.Publisher}");
         }

         Console.WriteLine();
      }

      public static async Task QueryBookAsync(string title)
      {
         Console.WriteLine(nameof(QueryBookAsync));

         try
         {
            await using var context = new BooksContext();
            var book = await context.Books.FirstOrDefaultAsync(b => b.Title == title)
               .ConfigureAwait(false);
            Console.WriteLine(book != null ? $"found book {book}" : "not found");
         }
         catch (InvalidOperationException ex) when (ex.HResult == -2146233079) // more than 1 element
         {
            Console.WriteLine(ex.Message);
         }

         Console.WriteLine();
      }

      public static async Task FilterBooksAsync(string title)
      {
         Console.WriteLine(nameof(FilterBooksAsync));

         await using var context = new BooksContext();
         var wroxBooks = await context.Books
            .Where(b => b.Title.Contains(title))
            .ToListAsync().ConfigureAwait(false);

         foreach (var currentBook in wroxBooks)
         {
            Console.WriteLine($"{currentBook.Title} {currentBook.Publisher}");
         }

         Console.WriteLine();
      }

      public static async Task ClientAndServerEvaluationAsync()
      {
         Console.WriteLine(nameof(ClientAndServerEvaluationAsync));

         try
         {
            await using var context = new BooksContext();
            var books = context.Books.Where(b => b.Title.StartsWith("Pro"))
               .OrderBy(b => b.Title)
               .Select(b => new
               {
                  b.Title,
                  Authors = string.Join(", ",
                     b.BookAuthors.Select(a => $"{a.Author.FirstName} {a.Author.LastName}").ToArray())
                  //  Authors = b.BookAuthors  // client evaluation
               });

            foreach (var b in books)
            {
               Console.WriteLine($"{b.Title} {b.Authors}");
            }
         }
         catch (InvalidOperationException ex) when (ex.HResult == -2146233079)
         {
            Console.WriteLine(ex.Message);
         }

         Console.WriteLine();
      }

      public static async Task RawSqlQueryAsync(string publisher)
      {
         Console.WriteLine(nameof(RawSqlQueryAsync));

         await using var context = new BooksContext();
         IList<Book> books = await context.Books.FromSqlRaw($"SELECT * FROM Books WHERE Publisher = '{publisher}'")
            .ToListAsync().ConfigureAwait(false);

         foreach (var b in books)
         {
            Console.WriteLine($"{b.Title} {b.Publisher}");
         }

         Console.WriteLine();
      }

      public static async Task UseEfFunctionsAsync(string titleSegment)
      {
         Console.WriteLine(nameof(UseEfFunctionsAsync));

         await using var context = new BooksContext();
         var likeExpression = $"%{titleSegment}%";

         IList<Book> books = await context.Books
            .Where(b => EF.Functions.Like(b.Title, likeExpression))
            .ToListAsync()
            .ConfigureAwait(false);
         foreach (var b in books)
         {
            Console.WriteLine($"{b.Title} {b.Publisher}");
         }

         Console.WriteLine();
      }

      public static void CompileQuery()
      {
         Console.WriteLine(nameof(CompileQuery));
         var query = EF.CompileQuery<BooksContext, string, Book>((context, publisher) =>
            context.Books.Where(b => b.Publisher == publisher));

         using (var context = new BooksContext())
         {
            var books = query(context, "Wrox Press");
            foreach (var b in books)
            {
               Console.WriteLine($"{b.Title} {b.Publisher}");
            }

            Console.WriteLine();
         }
      }

      public static async Task CompileQueryAsync()
      {
         Console.WriteLine(nameof(CompileQueryAsync));
         var query = EF.CompileAsyncQuery<BooksContext, string, Book>((context, publisher) =>
            context.Books.Where(b => b.Publisher == publisher));

         await using (var context = new BooksContext())
         {
            var books = query(context, "Wrox Press");
            await foreach (var currentBook in books)
            {
               Console.WriteLine($"{currentBook.Title} {currentBook.Publisher}");
            }

            Console.WriteLine();
         }
      }
   }
}