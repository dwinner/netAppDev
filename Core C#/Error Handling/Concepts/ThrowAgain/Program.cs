/**
 * Повторная генерация исключений
 */

using System;
using System.Collections;

namespace _02_ThrowAgain
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
            catch (ArgumentOutOfRangeException)
            {
               Console.WriteLine("Выполнить полезную работу и повторить исключение");
               throw;   // Сгенерировать заново перехваченное исключение
            }
            finally
            {
               Console.WriteLine("Очистка...");
            }
         }
         catch
         {
            Console.WriteLine("Готово");
         }

         Console.ReadKey();
      }
   }
}
