/*
 * The main point here is to demonstrate Many-To-Many relationship with
 * related topics
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _06_BooksSample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static _06_BooksSample.ColumnNames;

const string bookTitle = "sample book";
await CreateTheDatabaseAsync().ConfigureAwait(false);
await AddBookAsync("Professional C# 7", "Wrox Press").ConfigureAwait(false);
await AddBookAsync("Test", "Test").ConfigureAwait(false);
await AddBooksAsync().ConfigureAwait(false);
await QuerySamples.QueryAllBooksAsync().ConfigureAwait(false);
await QuerySamples.QueryAllBooksWithAsyncEnumerableAsync().ConfigureAwait(false);
await QuerySamples.QueryBookByKeyAsync(2).ConfigureAwait(false);
await UpdateBookAsync().ConfigureAwait(false);
await QuerySamples.QueryBookAsync("Professional C# 7 and .NET Core 2.0").ConfigureAwait(false);
await QuerySamples.FilterBooksAsync("Pro").ConfigureAwait(false);
await QuerySamples.UseEfFunctionsAsync("C#").ConfigureAwait(false);
await ConflictHandlingAsync().ConfigureAwait(false);
await DeleteBookAsync(2).ConfigureAwait(false);
await QueryDeletedBooksAsync().ConfigureAwait(false);
await QuerySamples.QueryBooksAsync().ConfigureAwait(false);
await QuerySamples.ClientAndServerEvaluationAsync().ConfigureAwait(false);
await QuerySamples.RawSqlQueryAsync("Wrox Press").ConfigureAwait(false);
QuerySamples.CompileQuery();
await QuerySamples.CompileQueryAsync().ConfigureAwait(false);
await DeleteDatabaseAsync().ConfigureAwait(false);

static async Task ConflictHandlingAsync()
{
   async Task PrepareBookAsync()
   {
      await using var context = new BooksContext();
      context.Books.Add(new Book(bookTitle, "Sample"));
      await context.SaveChangesAsync().ConfigureAwait(false);
   }

   await PrepareBookAsync().ConfigureAwait(false);

   // user 1
   var (booksContext1, book1) = await PrepareUpdateAsync()
      .ConfigureAwait(false);
   book1.Title = "updated from user 1";

   // user 2
   var (booksContext2, book2) = await PrepareUpdateAsync()
      .ConfigureAwait(false);
   book2.Title = "updated from user 2";

   Update(booksContext1, book1, "user 1");
   Update(booksContext2, book2, "user 2");

   booksContext1.Dispose();
   booksContext2.Dispose();

   await CheckUpdateAsync(book1.BookId).ConfigureAwait(false);
}

static async Task<(BooksContext context, Book book)> PrepareUpdateAsync()
{
   var context = new BooksContext();
   var book = await context.Books.Where(b => b.Title == bookTitle).FirstOrDefaultAsync()
      .ConfigureAwait(false);

   return (context, book);
}

static async Task CheckUpdateAsync(int id)
{
   await using var context = new BooksContext();
   var book = await context.Books.FindAsync(id).ConfigureAwait(false);
   Console.WriteLine($"this is the updated state: {book.Title}");
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

static async Task CreateTheDatabaseAsync()
{
   Console.WriteLine(nameof(CreateTheDatabaseAsync));

   await using var context = new BooksContext();
   var created = await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
   var creationInfo = created ? "created" : "exists";
   Console.WriteLine($"database {creationInfo}");

   Console.WriteLine();
}

static async Task DeleteDatabaseAsync()
{
   Console.WriteLine(nameof(DeleteDatabaseAsync));
   Console.Write("Delete the database? ");
   var input = Console.ReadLine();
   if (input?.ToLower() == "y")
   {
      await using var context = new BooksContext();
      var deleted = await context.Database.EnsureDeletedAsync()
         .ConfigureAwait(false);
      var deletionInfo = deleted ? "deleted" : "not deleted";
      Console.WriteLine($"database {deletionInfo}");
   }

   Console.WriteLine();
}

static async Task AddBookAsync(string title, string publisher)
{
   Console.WriteLine(nameof(AddBookAsync));

   await using var context = new BooksContext();
   var book = new Book(title, publisher);
   await context.Books.AddAsync(book).ConfigureAwait(false);
   var records = await context.SaveChangesAsync().ConfigureAwait(false);
   Console.WriteLine($"{records} record added");

   Console.WriteLine();
}

static async Task AddBooksAsync()
{
   Console.WriteLine(nameof(AddBooksAsync));

   await using var context = new BooksContext();

   var b1 = new Book("Professional C# 6 and .NET Core 1.0", "Wrox Press");
   var b2 = new Book("Professional C# 5 and .NET 4.5.1", "Wrox Press");
   var b3 = new Book("JavaScript for Kids", "Wrox Press");
   var b4 = new Book("HTML and CSS", "John Wiley");
   await context.Books.AddRangeAsync(b1, b2, b3, b4).ConfigureAwait(false);

   var a1 = new Author("Christian", "Nagel");
   var a2 = new Author("Jay", "Glynn");
   var a3 = new Author("Jon", "Duckett");
   var a4 = new Author("Nick", "Morgan");
   await context.Authors.AddRangeAsync(a1, a2, a3, a4).ConfigureAwait(false);

   var ba1 = new BookAuthor {Author = a1, Book = b1};
   var ba2 = new BookAuthor {Author = a1, Book = b2};
   var ba3 = new BookAuthor {Author = a2, Book = b2};
   var ba4 = new BookAuthor {Author = a3, Book = b4};
   var ba5 = new BookAuthor {Author = a4, Book = b3};
   await context.BookAuthors.AddRangeAsync(ba1, ba2, ba3, ba4, ba5).ConfigureAwait(false);

   var records = await context.SaveChangesAsync().ConfigureAwait(false);
   Console.WriteLine($"{records} records added");

   Console.WriteLine();
}

static async Task UpdateBookAsync()
{
   await using var context = new BooksContext();
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
   Console.WriteLine();
}

static async Task DeleteBookAsync(int id)
{
   await using var context = new BooksContext();
   var book = await context.Books.FindAsync(id)
      .ConfigureAwait(false);
   if (book == null)
   {
      return;
   }

   context.Books.Remove(book);
   var records = await context.SaveChangesAsync().ConfigureAwait(false);
   Console.WriteLine($"{records} books deleted");

   Console.WriteLine();
}

// this method only returns the deleted book if the global query filter
// is removed in the BooksContext class
static async Task QueryDeletedBooksAsync()
{
   await using var context = new BooksContext();
   IEnumerable<Book> deletedBooks =
      await context.Books
         .Where(b => EF.Property<bool>(b, IsDeleted))
         .ToListAsync().ConfigureAwait(false);

   //IEnumerable<Book> deletedBooks =
   //await context.Books.FromSqlRaw($"SELECT * FROM Books WHERE IsDeleted = 1").ToListAsync();

   foreach (var book in deletedBooks)
   {
      Console.WriteLine($"deleted: {book}");
   }
}