/**
 * Использование объектов POCO
 */

using System;
using System.Data.Entity;

namespace PocoDemo
{
   internal class Program
   {
      private static void Main()
      {
         using (var data = new BooksEntities())
         {
            data.Configuration.LazyLoadingEnabled = true;
            DbSet<Book> books = data.Books; // .Include("Authors");
            foreach (Book book in books)
            {
               Console.WriteLine("{0} {1}", book.Title, book.Publisher);
               foreach (Author author in book.Authors)
               {
                  Console.WriteLine("\t{0} {1}", author.FirstName, author.LastName);
               }
            }
         }
      }
   }
}