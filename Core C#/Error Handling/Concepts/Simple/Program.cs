/*
 * Сделано в SharpDevelop.
 * Пользователь: Denis
 * Дата: 30.11.2012
 * Время: 8:07
 *  
 */
using System;
using System.Collections;

namespace SimpleException
{
   class Program
   {
      public static void Main(string[] args)
      {
         Console.WriteLine("***** Simple Exception Example *****");
         Console.WriteLine("=> Creating a car and stepping on it!");
         Car myCar = new Car("Zippy", 20);
         myCar.CrankTunes(true);

         // Разгон до скорости, превышающей максимальный предел автомобиля, выдача исключения
         try
         {
            for (int i = 0; i < 10; i++)
               myCar.Accelerate(10);
         }
         catch (Exception e)
         {
            Console.WriteLine("\n*** Error! ***");
            Console.WriteLine("Member name: {0}", e.TargetSite);  // Имя члена
            Console.WriteLine("Class defining member: {0}",
                              e.TargetSite.DeclaringType);  // Класс, определяющий член
            Console.WriteLine("Member type: {0}",
                              e.TargetSite.MemberType);  // Тип члена
            Console.WriteLine("Message: {0}", e.Message);   // Сообщение
            Console.WriteLine("Source: {0}", e.Source);  // Источник
            Console.WriteLine("Stack: {0}", e.StackTrace);  // Вывод стека
            Console.WriteLine("Help link: {0}", e.HelpLink);   // Ссылка для справки

            // Дополнительная информация об исключении
            Console.WriteLine("\n-> Custom Data:");
            if (e.Data != null)
            {
               foreach (DictionaryEntry de in e.Data)
                  Console.WriteLine("-> {0}: {1}", de.Key, de.Value);
            }
         }

         Console.Write("Press any key to continue . . . ");
         Console.ReadKey(true);
      }
   }
}