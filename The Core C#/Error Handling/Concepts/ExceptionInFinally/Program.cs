/**
 * Исключение в блоке finally
 */

using System;
using System.Collections;

namespace _04_ExceptionInFinally
{
   class Program
   {
      static void Main(string[] args)
      {
         try
         {
            try
            {
               ArrayList list = new ArrayList();
               list.Add(1);
               Console.WriteLine("Элемент 10 = {0}", list[10]);
            }
            finally
            {
               Console.WriteLine("Очистка...");
               throw new Exception("Лучше сгенерировать исключение");
            }
         }
         catch (ArgumentOutOfRangeException)
         {
            Console.WriteLine("Аргумент вышел за допустимые пределы");
            throw;
         }
         catch
         {
            Console.WriteLine("Готово");
         }

         Console.ReadKey();
      }
   }
}
