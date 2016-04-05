/**
 * Использование ORM EF 5.x
 */

using System;
using System.Linq;
using _13_EntityFrameworkDemo.Orm;

namespace _13_EntityFrameworkDemo
{
   class Program
   {
      static void Main()
      {
         // Получить список всех сущностей
         using (var entities = new BookEntities())
         {
            foreach (var book in entities.Books)
            {
               Console.WriteLine("{0}, {1} {2}",
                  book.BookId, book.Title, book.PublishYear);
            }
         }

         // Создать/удалить новую сущность
         using (var entities = new BookEntities())
         {
            var bookEntity = new Books { Title = "Proffesional C# 4.5 And .NET 4.5 platform", PublishYear = 2012 };
            entities.Books.Add(bookEntity);
            entities.SaveChanges();

            entities.Books.Remove(bookEntity);
            entities.SaveChanges();
         }

         // Создание запросов
         using (var entities = new BookEntities())
         {
            var books = from book in entities.Books
                        where book.BookId == 1
                        select book;
            var firstOrDefault = books.FirstOrDefault();
            if (firstOrDefault != null)
               Console.WriteLine(firstOrDefault.BookId);
         }

         Console.ReadKey();
      }
   }
}
