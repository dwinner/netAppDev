/**
 * Запросы к EF-4.x-сущностям
 */

using System;
using System.Linq;

namespace LingToEntities
{
   class Program
   {
      static void Main()
      {
         // Запросить все книги
         BooksEntities bookContext = new BooksEntities();
         var books = from book in bookContext.BookSet
                     select book;
         foreach (var book in books)
         {
            // Загрузить сведения об авторе
            book.AuthorReference.Load();
            Console.WriteLine("{0}, {1} {2}",
               book.Title, book.Author.FirstName, book.Author.LastName);
         }
         Console.WriteLine();

         Console.WriteLine("Textbooks:");
         var textBooks = from book in bookContext.BookSet
                         where book is Textbook
                         select book as Textbook;
         foreach (var textBook in textBooks)
         {
            // Загрузить информацию об авторе
            textBook.AuthorReference.Load();
            Console.WriteLine("{0}, {1} {2} - {3}",
               textBook.Title,
               textBook.Author.FirstName,
               textBook.Author.LastName,
               textBook.Subject);
         }
         Console.WriteLine();
         
         // Добавление сущности
         Author me = new Author { FirstName = "Ben", LastName = "Watson" };
         Book myBook = new Book { Author = me, Title = "C# 4.0 How-To" };

         bookContext.AddToBookSet(myBook);

         bookContext.SaveChanges();
         Console.WriteLine("Added my book:");
         foreach (var book in from b in bookContext.BookSet select b)
         {
            book.AuthorReference.Load();
            Console.WriteLine("{0}, {1} {2}",
               book.Title, book.Author.FirstName, book.Author.LastName);
         }

         // Удаление сущности

         bookContext.DeleteObject(myBook);
         bookContext.DeleteObject(me);
         bookContext.SaveChanges();
         Console.ReadKey();
      }
   }
}
