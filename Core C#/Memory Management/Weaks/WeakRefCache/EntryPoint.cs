/**
 * Кэш, доступный сборщику мусора
 */

using System;

namespace WeakRefCache
{
   public static class EntryPoint
   {
      public static void Main()
      {
         var bookCache = new WeakCache<int, Book>();
         var rand = new Random();
         const int numBooks = 100;

         // Добавить в кэш информацию о книгах

         for (int i = 0; i < numBooks; i++)
         {
            bookCache.Add(i, GetBookFromDb(i));
         }

         // Искать книги случайным образом и подсчитывать количество
         // "промахов", то есть названий, отсутствующих в кэше
         
         Console.WriteLine("Looking up books...hit any key to stop");
         long lookups = 0, misses = 0;
         while (!Console.KeyAvailable)
         {
            ++lookups;
            int id = rand.Next(0, numBooks);
            Book book = bookCache.GetObject(id);
            if (book == null)
            {
               ++misses;
               book = GetBookFromDb(id);
            }
            else
            {
               // Увеличить объем выделенной памяти для повышения
               // вероятности запуска сборщика мусора

               GC.AddMemoryPressure(100);
            }
            bookCache.Add(id, book);
         }

         Console.ReadKey();
         Console.WriteLine("{0:N0} lookups, {1:N0} misses",
            lookups, misses);
         Console.ReadLine();
      }

      private static Book GetBookFromDb(int id)  // Note: Имитация обращения к базе данных
      {
         return new Book
         {
            Id = id,
            Title = string.Format("Book{0}", id),
            Author = string.Format("Author{0}", id)
         };
      }
   }   
}