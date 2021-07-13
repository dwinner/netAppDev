using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _06_BooksSample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static _06_BooksSample.ColumnNames;

const string bookTitle = "sample book";
await CreateTheDatabaseAsync();
await AddBookAsync("Professional C# 7", "Wrox Press");
await AddBookAsync("Test", "Test");
await AddBooksAsync();
await QuerySamples.QueryAllBooksAsync();
await QuerySamples.QueryAllBooksWithAsyncEnumerableAsync();
await QuerySamples.QueryBookByKeyAsync(2);
await UpdateBookAsync();
await QuerySamples.QueryBookAsync("Professional C# 7 and .NET Core 2.0");
await QuerySamples.FilterBooksAsync("Pro");
await QuerySamples.UseEfFunctionsAsync("C#");
ConflictHandling();
await DeleteBookAsync(2);
await QueryDeletedBooksAsync();
await QuerySamples.QueryBooksAsync();
QuerySamples.ClientAndServerEvaluation();
await QuerySamples.RawSqlQueryAsync("Wrox Press");
QuerySamples.CompileQuery();
await DeleteDatabaseAsync();

static void ConflictHandling()
{
   void PrepareBook()
   {
      using (var context = new BooksContext())
      {
         context.Books.Add(new Book(bookTitle, "Sample"));
         context.SaveChanges();
      }
   }

   PrepareBook();

   // user 1
   var tuple1 = PrepareUpdate();
   tuple1.book.Title = "updated from user 1";

   // user 2
   var tuple2 = PrepareUpdate();
   tuple2.book.Title = "updated from user 2";

   Update(tuple1.context, tuple1.book, "user 1");
   Update(tuple2.context, tuple2.book, "user 2");

   tuple1.context.Dispose();
   tuple2.context.Dispose();

   CheckUpdate(tuple1.book.BookId);
}

static (BooksContext context, Book book) PrepareUpdate()
{
   var context = new BooksContext();
   var book = context.Books.Where(b => b.Title == bookTitle).FirstOrDefault();
   return (context, book);
}

static void CheckUpdate(int id)
{
   using (var context = new BooksContext())
   {
      var book = context.Books.Find(id);
      Console.WriteLine($"this is the updated state: {book.Title}");
   }
}

static void Update(BooksContext context, Book book, string user)
{
   var records = context.SaveChanges();
   Console.WriteLine($"{user}: {records} record updated from {user}");
}

static void ShowChanges(int id, EntityEntry entity)
{
   void ShowChange(PropertyEntry propertyEntry) =>
      Console.WriteLine(
         $"id: {id}, current: {propertyEntry.CurrentValue}, original: {propertyEntry.OriginalValue}, modified: {propertyEntry.IsModified}");

   ShowChange(entity.Property("Title"));
   ShowChange(entity.Property("Publisher"));
}

async Task CreateTheDatabaseAsync()
{
   Console.WriteLine(nameof(CreateTheDatabaseAsync));
   using (var context = new BooksContext())
   {
      var created = await context.Database.EnsureCreatedAsync();
      var creationInfo = created ? "created" : "exists";
      Console.WriteLine($"database {creationInfo}");
   }

   Console.WriteLine();
}

async Task DeleteDatabaseAsync()
{
   Console.WriteLine(nameof(DeleteDatabaseAsync));
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

   Console.WriteLine();
}

async Task AddBookAsync(string title, string publisher)
{
   Console.WriteLine(nameof(AddBookAsync));
   using (var context = new BooksContext())
   {
      var book = new Book(title, publisher);
      await context.Books.AddAsync(book);
      var records = await context.SaveChangesAsync();

      Console.WriteLine($"{records} record added");
   }

   Console.WriteLine();
}

async Task AddBooksAsync()
{
   Console.WriteLine(nameof(AddBooksAsync));
   using (var context = new BooksContext())
   {
      var b1 = new Book("Professional C# 6 and .NET Core 1.0", "Wrox Press");
      var b2 = new Book("Professional C# 5 and .NET 4.5.1", "Wrox Press");
      var b3 = new Book("JavaScript for Kids", "Wrox Press");
      var b4 = new Book("HTML and CSS", "John Wiley");
      await context.Books.AddRangeAsync(b1, b2, b3, b4);

      var a1 = new Author("Christian", "Nagel");
      var a2 = new Author("Jay", "Glynn");
      var a3 = new Author("Jon", "Duckett");
      var a4 = new Author("Nick", "Morgan");
      await context.Authors.AddRangeAsync(a1, a2, a3, a4);

      var ba1 = new BookAuthor {Author = a1, Book = b1};
      var ba2 = new BookAuthor {Author = a1, Book = b2};
      var ba3 = new BookAuthor {Author = a2, Book = b2};
      var ba4 = new BookAuthor {Author = a3, Book = b4};
      var ba5 = new BookAuthor {Author = a4, Book = b3};
      await context.BookAuthors.AddRangeAsync(ba1, ba2, ba3, ba4, ba5);

      var records = await context.SaveChangesAsync();

      Console.WriteLine($"{records} records added");
   }

   Console.WriteLine();
}

async Task UpdateBookAsync()
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

async Task DeleteBookAsync(int id)
{
   using (var context = new BooksContext())
   {
      var b = await context.Books.FindAsync(id);
      if (b == null)
      {
         return;
      }

      context.Books.Remove(b);
      var records = await context.SaveChangesAsync();
      Console.WriteLine($"{records} books deleted");
   }

   Console.WriteLine();
}

// this method only returns the deleted book if the global query filter
// is removed in the BooksContext class
async Task QueryDeletedBooksAsync()
{
   using (var context = new BooksContext())
   {
      IEnumerable<Book> deletedBooks =
         await context.Books
            .Where(b => EF.Property<bool>(b, IsDeleted))
            .ToListAsync();
      //IEnumerable<Book> deletedBooks =
      //await context.Books.FromSql($"SELECT * FROM Books WHERE IsDeleted = 1").ToListAsync();
      foreach (var book in deletedBooks)
      {
         Console.WriteLine($"deleted: {book}");
      }
   }
}