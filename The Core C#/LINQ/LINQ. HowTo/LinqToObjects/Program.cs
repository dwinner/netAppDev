/**
 * Запрос к коллекции объектов
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToObjects
{
   class Book
   {
      public string Title { get; set; }
      public int AuthorId { get; set; }
      public int PublishYear { get; set; }

      public Book(string title, int authorId, int year)
      {
         Title = title;
         AuthorId = authorId;
         PublishYear = year;
      }

      public override string ToString()
      {
         return string.Format("{0} - {1}", Title, PublishYear);
      }
   }

   class Author
   {
      public int Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }

      public Author(int id, string firstName, string lastName)
      {
         Id = id;
         FirstName = firstName;
         LastName = lastName;
      }
   }

   class Program
   {
      static void Main()
      {
         List<Book> books = new List<Book>
         {
            new Book("Le Rhin", 1, 1842),
            new Book("Les Burgraves",1, 1843),
            new Book("Napoléon le Petit",1, 1852),
            new Book("Les Châtiments",1, 1853),
            new Book("Les Contemplations", 1, 1856),
            new Book("Les Misérables", 1, 1862)
         };

         List<Author> authors = new List<Author> { new Author(1, "Victor", "Hugo") };

         PrintBooks("Original list", books);

         // Note: простейший запрос
         var allBooks = from book in books
                        select book;
         PrintBooks("All books", allBooks);

         // Note: упорядочивание результатов
         var ordered = from book in books
                       orderby book.Title descending
                       select book;

         PrintBooks("Ordered by titles", ordered);

         // Note: фильтрование коллекции
         var before1850 = from book in books
                          where book.PublishYear < 1850
                          select book;

         PrintBooks("Books before 1850", before1850);

         var dateRange = from book in books
                         where book.PublishYear >= 1850 && book.PublishYear <= 1855
                         select book;

         PrintBooks("Books between 1850 and 1855", dateRange);

         // Note: проекции
         var justTitlesAfter1850 = from book in books
                                   where book.PublishYear > 1850
                                   select book.Title;

         PrintBooks("Titles of books after 1850", justTitlesAfter1850);

         // Note: объединение
         var withAuthors = from book in books
                           join author in authors
                              on book.AuthorId equals author.Id
                           select new { Book = book.Title, Author = author.FirstName + " " + author.LastName };
         Console.WriteLine("Join with authors:");
         foreach (var bookAndAuthor in withAuthors)
         {
            Console.WriteLine("{0}, {1}", bookAndAuthor.Book, bookAndAuthor.Author);
         }

         // Note: группировка
         IEnumerable<IGrouping<int, Book>> booksByYear = from book in books
                                                         group book by book.PublishYear;
         foreach (var yearGroup in booksByYear)
         {
            Console.WriteLine(yearGroup.Key);
            foreach (var book in yearGroup)
            {
               Console.WriteLine(book);
            }
         }

         Console.ReadKey();
      }

      static void PrintBooks<T>(string title, IEnumerable<T> books)
      {
         Console.WriteLine("{0}:", title);
         foreach (T book in books)
         {
            Console.WriteLine(book.ToString());
         }
         Console.WriteLine();
      }
   }
}
