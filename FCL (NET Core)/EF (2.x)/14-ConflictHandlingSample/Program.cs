using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace _14_ConflictHandlingSample
{
   internal static class Program
   {
      private const string BookTitle = "sample book";

      private static void Main()
      {
         CreateDatabase();
         AddBook();
         ConflictHandling();
         DeleteDatabase();
      }

      private static void ConflictHandling()
      {
         // user 1
         var (context1, book) = PrepareUpdate();
         book.Title = "user 1 wins";

         // user 2
         var (context2, book2) = PrepareUpdate();
         book2.Title = "user 2 wins";

         Update(context1, book, "user 1");
         Update(context2, book2, "user 2");

         context1.Dispose();
         context2.Dispose();

         CheckUpdate(book.BookId);
      }

      private static void CheckUpdate(int id)
      {
         using var context = new BooksContext();
         var book = context.Books.Find(id);
         Console.WriteLine($"this is the updated state: {book.Title}");
      }

      private static void Update(DbContext context, Book book, string user)
      {
         try
         {
            Console.WriteLine($"{user}: updating id {book.BookId}, timestamp {book.TimeStamp.StringOutput()}");
            ShowChanges(book.BookId, context.Entry(book));

            var records = context.SaveChanges();
            Console.WriteLine($"{user}: updated {book.TimeStamp.StringOutput()}");
            Console.WriteLine($"{user}: {records} record(s) updated while updating {book.Title}");
         }
         catch (DbUpdateConcurrencyException ex)
         {
            Console.WriteLine($"{user} update failed with {book.Title}");
            Console.WriteLine($"{user} error: {ex.Message}");
            foreach (var entry in ex.Entries)
            {
               if (entry.Entity is Book b)
               {
                  Console.WriteLine($"{b.Title} {b.TimeStamp.StringOutput()}");
                  ShowChanges(book.BookId, context.Entry(book));
               }
            }
         }
      }

      private static void ShowChanges(int id, EntityEntry entity)
      {
         void ShowChange(PropertyEntry propertyEntry) =>
            Console.WriteLine(
               $"id: {id}, current: {propertyEntry.CurrentValue}," +
               $" original: {propertyEntry.OriginalValue}," +
               $" modified: {propertyEntry.IsModified}");

         ShowChange(entity.Property("Title"));
         ShowChange(entity.Property("Publisher"));
      }

      private static (BooksContext context, Book book) PrepareUpdate()
      {
         var context = new BooksContext();
         var book = context.Books.FirstOrDefault(b => b.Title == BookTitle);
         return (context, book);
      }

      private static void DeleteDatabase()
      {
         Console.WriteLine(nameof(DeleteDatabase));
         Console.Write("Delete the database? ");
         var input = Console.ReadLine();
         if (input?.ToLower() == "y")
         {
            using var context = new BooksContext();
            var deleted = context.Database.EnsureDeleted();
            var deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
            Console.WriteLine();
         }
      }

      private static void AddBook()
      {
         using var context = new BooksContext();
         var b = new Book {Title = BookTitle, Publisher = "Sample"};

         context.Add(b);
         var records = context.SaveChanges();

         Console.WriteLine($"{records} record(s) added");
         Console.WriteLine();
      }

      private static void CreateDatabase()
      {
         Console.WriteLine(nameof(CreateDatabase));
         using var context = new BooksContext();
         var created = context.Database.EnsureCreated();
         var creationInfo = created ? "created" : "exists";
         Console.WriteLine($"database {creationInfo}");
         Console.WriteLine();
      }
   }
}