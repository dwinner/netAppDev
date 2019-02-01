/**
 * Перечисления и строки
 */

using System;

namespace EnumsAndStrings
{
   class Program
   {
      static void Main()
      {
         // note: Получение списка значений перечисления
         foreach (BookGenres genre in Enum.GetValues(typeof(BookGenres)))
         {
            Console.WriteLine("\t" + Enum.GetName(typeof(BookGenres), genre));
         }

         // note: Получение списка строк в перечислении
         foreach (var enumName in Enum.GetNames(typeof(BookGenres)))
         {
            Console.WriteLine(enumName);
         }

         // note: Преобразование строки в перечисление
         const string hardCoverString = "hardcover";
         BookBinding goodBinding, badBinding;
         // Этот код отработает успешно
         bool canParse = Enum.TryParse(hardCoverString, out goodBinding);
         // Здесь ничего не получится
         canParse = Enum.TryParse("garbage", out badBinding);

         // note: Преобразование строки в набор флагов
         const string flagString = "Vampire, Mystery, ScienceFiction, Vampire";
         BookGenres flagEnum;
         if (Enum.TryParse(flagString, out flagEnum))
         {
            Console.WriteLine("Parsed \"{0}\" into {1}", flagString, flagEnum);
         }

         Console.ReadKey(true);
      }
   }

   [Flags]
   enum BookGenres : byte
   {
      None = 0x00,
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
