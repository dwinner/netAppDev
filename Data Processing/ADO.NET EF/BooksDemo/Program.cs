/**
 * Простой доступ к базе из O/R-mapper'а
 */

using System;

namespace BooksDemo
{
   internal class Program
   {
      private static void Main()
      {
         using (var bookDatabaseEntities = new BookDatabaseEntities())
         {
            foreach (Book book in bookDatabaseEntities.Books)
            {
               Console.WriteLine("{0}, {1}", book.Title, book.Publisher);
            }
         }
      }
   }
}