/**
 * Объявление флагов в виде перечисления
 */

using System;

namespace EnumFlags
{
   class Program
   {
      static void Main(string[] args)
      {
         // Перечисления, не помеченные атрибутом Flags
         const BookBinding binding = BookBinding.Hardcover;
         const BookBinding doubleBinding = BookBinding.Hardcover | BookBinding.Paperback;
         Console.WriteLine("Binding: {0}", binding);
         Console.WriteLine("Double Binding: {0}", doubleBinding); // 3

         // Перечисления, помеченные атрибутом Flags
         const BookGenres genres = BookGenres.Vampire | BookGenres.Fantasy | BookGenres.Romance;
         Console.WriteLine("Genres: {0}", genres); // Ok

         // NOTE: Выяснение, установлен ли флаг
         const BookGenres bookGenres = BookGenres.Vampire | BookGenres.Fantasy;
         //bool isVampire = ((bookGenres & BookGenres.Vampire) != 0);
         bool isVampireAlt = bookGenres.HasFlag(BookGenres.Vampire); // Вариант для .NET 4+
         Console.WriteLine(isVampireAlt);
         Console.ReadKey();
      }
   }

   [Flags]
   enum BookGenres
   {
      None = 0,
      ScienceFiction = 0x01,
      Crime = 0x02,
      Romance = 0x04,
      History = 0x08,
      Science = 0x10,
      Mystery = 0x20,
      Fantasy = 0x40,
      Vampire = 0x80
   }

   enum BookBinding
   {
      None,
      Hardcover,
      Paperback
   }
}
