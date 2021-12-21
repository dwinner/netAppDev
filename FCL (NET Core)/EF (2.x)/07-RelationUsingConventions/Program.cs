using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _07_RelationUsingConventions
{
   internal static class Program
   {
      private static async Task Main()
      {
         await CreateDatabaseAsync().ConfigureAwait(false);
         await AddBooksAsync().ConfigureAwait(false);
         await NoImplicitLoadingWithEfCore2Async()
            .ConfigureAwait(false); // Entity Framework 6 does support implicit loading
         await ExplicitLoadingAsync().ConfigureAwait(false);
         await EagerLoadingAsync().ConfigureAwait(false);
         await BooksForAuthorAsync().ConfigureAwait(false);
         await DeleteDatabaseAsync().ConfigureAwait(false);
      }

      private static async Task NoImplicitLoadingWithEfCore2Async()
      {
         Console.WriteLine(nameof(NoImplicitLoadingWithEfCore2Async));

         await using var context = new BooksContext();

         /*var book1 = (from b in context.Books
            from c in b.Chapters
            where c.Title.StartsWith("Entity")
            select b).FirstOrDefault();*/

         var book = context.Books
            .SelectMany(b => b.Chapters,
               (b, chapters) =>
                  new
                  {
                     Book = b,
                     Chapters = chapters
                  }) // defining expression trees does not support tuples (yet)
            .Where(bc => bc.Chapters.Title.StartsWith("Entity"))
            .Select(bc => bc.Book).FirstOrDefault();

         //var book = context.Books
         //    .SelectMany(b => b.Chapters, (b, chapters) => (Book: b, Chapters: chapters))  // defining expression trees does not support tuples (yet) - see https://github.com/dotnet/roslyn/issues/12897
         //    .Where(bc => bc.Chapters.Title.StartsWith("Entity"))
         //    .Select(bc => bc.Book).FirstOrDefault();

         if (book is {Chapters: null})
         {
            Console.WriteLine(
               "Chapters are not implicitly loaded with EF Core, this is different from Entity Framework");
         }

         if (book != null)
         {
            Console.WriteLine(book.Title);

            if (!context.Entry(book).Collection(b => b.Chapters).IsLoaded)
            {
               await context.Entry(book).Collection(b => b.Chapters).LoadAsync()
                  .ConfigureAwait(false);
            }

            if (book.Chapters != null)
            {
               foreach (var chapter in book.Chapters)
               {
                  Console.WriteLine($"{chapter.Number}. {chapter.Title}");
               }
            }
         }

         Console.WriteLine();
      }

      private static async Task BooksForAuthorAsync()
      {
         await using var context = new BooksContext();
         var author = await context.Users.Include(u => u.AuthoredBooks)
            .FirstOrDefaultAsync(u => u.Name == "Christian Nagel")
            .ConfigureAwait(false);
         if (author != null)
         {
            Console.WriteLine(author.Name);
            foreach (var b in author.AuthoredBooks)
            {
               Console.WriteLine(b.Title);
            }
         }
      }

      private static async Task ExplicitLoadingAsync()
      {
         Console.WriteLine(nameof(ExplicitLoadingAsync));

         await using (var context = new BooksContext())
         {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Title.StartsWith("Professional C# 7"))
               .ConfigureAwait(false);
            if (book != null)
            {
               Console.WriteLine(book.Title);

               await context.Entry(book).Collection(b => b.Chapters).LoadAsync()
                  .ConfigureAwait(false);
               await context.Entry(book).Reference(b => b.Author).LoadAsync()
                  .ConfigureAwait(false);

               Console.WriteLine(book.Author.Name);

               foreach (var chapter in book.Chapters)
               {
                  Console.WriteLine($"{chapter.Number}. {chapter.Title}");
               }
            }
         }

         Console.WriteLine();
      }

      private static async Task EagerLoadingAsync()
      {
         Console.WriteLine(nameof(EagerLoadingAsync));

         await using var context = new BooksContext();
         var book = await context.Books
            .Include(b => b.Chapters)
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Title.StartsWith("Professional C# 7"))
            .ConfigureAwait(false);
         if (book != null)
         {
            Console.WriteLine(book.Title);
            Console.WriteLine(book.Author.Name);
            foreach (var chapter in book.Chapters)
            {
               Console.WriteLine($"{chapter.Number}. {chapter.Title}");
            }
         }

         Console.WriteLine();
      }

      private static async Task DeleteDatabaseAsync()
      {
         Console.Write("Delete the database? ");
         var input = Console.ReadLine();
         if (input?.ToLower() != "y")
         {
            return;
         }

         await using var context = new BooksContext();
         await context.Database.EnsureDeletedAsync().ConfigureAwait(false);
      }

      private static async Task CreateDatabaseAsync()
      {
         await using var context = new BooksContext();
         var created = await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
         Console.WriteLine($"database created: {created}");
      }

      private static async Task AddBooksAsync()
      {
         Console.WriteLine(nameof(AddBooksAsync));

         await using var context = new BooksContext();
         var author = new User
         {
            Name = "Christian Nagel"
         };
         var b1 = new Book
         {
            Title = "Professional C# 7 and .NET Core 2.0",
            Author = author
         };
         var c1 = new Chapter
         {
            Title = ".NET Applications and Tools",
            Number = 1,
            Book = b1
         };
         var c2 = new Chapter
         {
            Title = "Core C#",
            Number = 2,
            Book = b1
         };
         var c3 = new Chapter
         {
            Title = "Entity Framework Core",
            Number = 28,
            Book = b1
         };

         await context.Books.AddAsync(b1).ConfigureAwait(false);
         await context.Users.AddAsync(author).ConfigureAwait(false);
         await context.Chapters.AddRangeAsync(c1, c2, c3).ConfigureAwait(false);

         var records = await context.SaveChangesAsync().ConfigureAwait(false);
         b1.Chapters.AddRange(new[] {c1, c2, c3});
         Console.WriteLine($"{records} records added");

         Console.WriteLine();
      }
   }
}