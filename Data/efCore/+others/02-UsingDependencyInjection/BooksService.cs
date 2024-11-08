﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _02_UsingDependencyInjection
{
   public class BooksService
   {
      private readonly BooksContext _booksContext;

      public BooksService(BooksContext context) => _booksContext = context;

      public async Task AddBooksAsync()
      {
         var b1 = new Book
         {
            Title = "Professional C# 6 and .NET Core 1.0",
            Publisher = "Wrox Press"
         };
         var b2 = new Book
         {
            Title = "Professional C# 5.0 and .NET 4.5.1",
            Publisher = "Wrox Press"
         };
         var b3 = new Book
         {
            Title = "JavaScript for Kids",
            Publisher = "Wrox Press"
         };
         var b4 = new Book
         {
            Title = "Web Design with HTML and CSS",
            Publisher = "For Dummies"
         };

         await _booksContext.AddRangeAsync(b1, b2, b3, b4).ConfigureAwait(false);
         var records = await _booksContext.SaveChangesAsync()
            .ConfigureAwait(false);

         Console.WriteLine($"{records} records added");
      }

      public async Task ReadBooksAsync()
      {
         var books = await _booksContext.Books.ToListAsync()
            .ConfigureAwait(false);
         foreach (var b in books)
         {
            Console.WriteLine($"{b.Title} {b.Publisher}");
         }

         Console.WriteLine();
      }
   }
}