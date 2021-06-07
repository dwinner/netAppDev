/*
 * Simple database operations
 */

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _01_Intro
{
   internal class Program
   {
      private static async Task Main()
      {
         var program = new Program();
         await program.CreateTheDatabaseAsync().ConfigureAwait(false);
         await program.AddBookAsync("Professional C# 7", "Wrox Press").ConfigureAwait(false);
         await program.AddBooksAsync().ConfigureAwait(false);
         await program.ReadBooksAsync().ConfigureAwait(false);
         await program.QueryBooksAsync().ConfigureAwait(false);
         await program.UpdateBookAsync().ConfigureAwait(false);
         await program.DeleteBooksAsync().ConfigureAwait(false);
         await program.DeleteDatabaseAsync().ConfigureAwait(false);
      }

      private async Task CreateTheDatabaseAsync()
      {
         await using var context = new BooksContext();
         var created = await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
         var creationInfo = created ? "created" : "exists";
         Console.WriteLine($"database {creationInfo}");
      }

      private async Task DeleteDatabaseAsync()
      {
         Console.Write("Delete the database? ");
         var input = Console.ReadLine();
         if (input.ToLower() == "y")
         {
            await using var context = new BooksContext();
            var deleted = await context.Database.EnsureDeletedAsync().ConfigureAwait(false);
            var deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
         }
      }

      private async Task AddBookAsync(string title, string publisher)
      {
         await using (var context = new BooksContext())
         {
            var book = new Book {Title = title, Publisher = publisher};
            await context.Books.AddAsync(book).ConfigureAwait(false);
            var records = await context.SaveChangesAsync().ConfigureAwait(false);

            Console.WriteLine($"{records} record added");
         }

         Console.WriteLine();
      }

      private async Task AddBooksAsync()
      {
         await using (var context = new BooksContext())
         {
            var b1 = new Book {Title = "Professional C# 6 and .NET Core 1.0", Publisher = "Wrox Press"};
            var b2 = new Book {Title = "Professional C# 5 and .NET 4.5.1", Publisher = "Wrox Press"};
            var b3 = new Book {Title = "JavaScript for Kids", Publisher = "Wrox Press"};
            var b4 = new Book {Title = "Web Design with HTML and CSS", Publisher = "For Dummies"};
            await context.Books.AddRangeAsync(b1, b2, b3, b4).ConfigureAwait(false);
            var records = await context.SaveChangesAsync().ConfigureAwait(false);

            Console.WriteLine($"{records} records added");
         }

         Console.WriteLine();
      }

      private async Task ReadBooksAsync()
      {
         await using (var context = new BooksContext())
         {
            var books = await context.Books.ToListAsync().ConfigureAwait(false);
            foreach (var b in books)
            {
               Console.WriteLine($"{b.Title} {b.Publisher}");
            }
         }

         Console.WriteLine();
      }

      private async Task QueryBooksAsync()
      {
         await using (var context = new BooksContext())
         {
            var wroxBooks = await context.Books
               .Where(b => b.Publisher == "Wrox Press")
               .ToListAsync().ConfigureAwait(false);

            // comment the previous lines and uncomment the next lines to try the LINQ query syntax
            //var wroxBooks = await (from b in context.Books
            //                         where b.Publisher == "Wrox Press"
            //                         select b).ToListAsync();

            foreach (var b in wroxBooks)
            {
               Console.WriteLine($"{b.Title} {b.Publisher}");
            }
         }

         Console.WriteLine();
      }

      private async Task UpdateBookAsync()
      {
         await using (var context = new BooksContext())
         {
            var records = 0;
            var book = await context.Books
               .Where(b => b.Title == "Professional C# 7")
               .FirstOrDefaultAsync().ConfigureAwait(false);
            if (book != null)
            {
               book.Title = "Professional C# 7 and .NET Core 2.0";
               records = await context.SaveChangesAsync().ConfigureAwait(false);
            }

            Console.WriteLine($"{records} record updated");
         }

         Console.WriteLine();
      }

      private async Task DeleteBooksAsync()
      {
         await using (var context = new BooksContext())
         {
            var books = await context.Books.ToListAsync().ConfigureAwait(false);
            context.Books.RemoveRange(books);
            var records = await context.SaveChangesAsync().ConfigureAwait(false);
            Console.WriteLine($"{records} records deleted");
         }

         Console.WriteLine();
      }
   }
}