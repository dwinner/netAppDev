﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BooksDemo.Data
{
   public class BookFactory
   {
      private readonly ObservableCollection<Book> _books = new ObservableCollection<Book>();

      public BookFactory()
      {
         _books.Add(new Book(
            "Professional C# 4 with .NET 4",
            "Wrox Press", "978-0-470-50225-9",
            "Christian Nagel", "Bill Evjen", "Jay Glynn", "Karli Watson", "Morgan Skinner"));
         _books.Add(new Book(
            "Professional C# 2012 and .NET 4.5",
            "Wrox Press", "978-1-118-31442-5 ",
            "Christian Nagel", "Jay Glynn", "Morgan Skinner"));
         _books.Add(new Book(
            "Beginning Visual C# 2010",
            "Wrox Press",
            "978-0-470-50226-6",
            "Karli Watson", "Christian Nagel", "Jacob Hammer Pedersen", "Jon D. Reid", "Morgan Skinner"));
         _books.Add(new Book(
            "Windows 8 Secrets",
            "Wiley",
            "978-1-118-20413-9",
            "Paul Thurrott", "Rafael Rivera"));
         _books.Add(new Book(
            "C# 2012 All-in-One for Dummies",
            "For Dummies",
            "978-0-470-19109-5",
            "Bill Sempf"));
      }

      public Book GetTheBook() => _books[0];

      public void AddBook(Book aBook) => _books.Add(aBook);

      public IEnumerable<Book> GetBooks() => _books;
   }
}