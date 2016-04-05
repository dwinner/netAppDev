/**
 * Трансляция исключений
 */

using System;
using System.Collections;

namespace _03_ExceptionTranslating
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
            catch (ArgumentOutOfRangeException x)
            {
               Console.WriteLine("Выполнить полезную работу и транслировать исключение");
               throw new MyException("Лучше сгенерировать исключение", x);
            }
            finally
            {
               Console.WriteLine("Очистка...");
            }
         }
         catch (Exception x)
         {
            Console.WriteLine(x);
            Console.WriteLine("Готово");
         }

         Console.ReadKey();
      }
   }

   public class MyException : Exception
   {
      public MyException(string reason, Exception innerEx)
         : base(reason, innerEx)
      { }
   }
}
