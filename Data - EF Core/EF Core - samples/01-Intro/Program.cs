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
         await program.CreateTheDatabaseAsync();
         await program.AddBookAsync("Professional C# 7", "Wrox Press");
         await program.AddBooksAsync();
         await program.ReadBooksAsync();
         await program.QueryBooksAsync();
         await program.UpdateBookAsync();
         await program.DeleteBooksAsync();
         await program.DeleteDatabaseAsync();
      }

      private async Task CreateTheDatabaseAsync()
      {
         using (var context = new BooksContext())
         {
            var created = await context.Database.EnsureCreatedAsync();
            var creationInfo = created ? "created" : "exists";
            Console.WriteLine($"database {creationInfo}");
         }
      }

      private async Task DeleteDatabaseAsync()
      {
         Console.Write("Delete the database? ");
         var input = Console.ReadLine();
         if (input.ToLower() == "y")
         {
            using (var context = new BooksContext())
            {
               var deleted = await context.Database.EnsureDeletedAsync();
               var deletionInfo = deleted ? "deleted" : "not deleted";
               Console.WriteLine($"database {deletionInfo}");
            }
         }
      }

      private async Task AddBookAsync(string title, string publisher)
      {
         using (var context = new BooksContext())
         {
            var book = new Book {Title = title, Publisher = publisher};
            await context.Books.AddAsync(book);
            var records = await context.SaveChangesAsync();

            Console.WriteLine($"{records} record added");
         }

         Console.WriteLine();
      }

      private async Task AddBooksAsync()
      {
         using (var context = new BooksContext())
         {
            var b1 = new Book {Title = "Professional C# 6 and .NET Core 1.0", Publisher = "Wrox Press"};
            var b2 = new Book {Title = "Professional C# 5 and .NET 4.5.1", Publisher = "Wrox Press"};
            var b3 = new Book {Title = "JavaScript for Kids", Publisher = "Wrox Press"};
            var b4 = new Book {Title = "Web Design with HTML and CSS", Publisher = "For Dummies"};
            await context.Books.AddRangeAsync(b1, b2, b3, b4);
            var records = await context.SaveChangesAsync();

            Console.WriteLine($"{records} records added");
         }

         Console.WriteLine();
      }

      private async Task ReadBooksAsync()
      {
         using (var context = new BooksContext())
         {
            var books = await context.Books.ToListAsync();
            foreach (var b in books)
            {
               Console.WriteLine($"{b.Title} {b.Publisher}");
            }
         }

         Console.WriteLine();
      }

      private async Task QueryBooksAsync()
      {
         using (var context = new BooksContext())
         {
            var wroxBooks = await context.Books
               .Where(b => b.Publisher == "Wrox Press")
               .ToListAsync();

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
         using (var context = new BooksContext())
         {
            var records = 0;
            var book = await context.Books
               .Where(b => b.Title == "Professional C# 7")
               .FirstOrDefaultAsync();
            if (book != null)
            {
               book.Title = "Professional C# 7 and .NET Core 2.0";
               records = await context.SaveChangesAsync();
            }

            Console.WriteLine($"{records} record updated");
         }

         Console.WriteLine();
      }

      private async Task DeleteBooksAsync()
      {
         using (var context = new BooksContext())
         {
            var books = await context.Books.ToListAsync();
            context.Books.RemoveRange(books);
            var records = await context.SaveChangesAsync();
            Console.WriteLine($"{records} records deleted");
         }

         Console.WriteLine();
      }
   }
}