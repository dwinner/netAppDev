/**
 * Исключения в финализаторах
 */

using System;

namespace _05_ExceptionsInFinalizers
{
   class Program
   {
      static void Main(string[] args)
      {
         Person person = new Employee();
         person = null;
         Console.ReadKey();
      }
   }

   public class Person
   {
      ~Person()
      {
         Console.WriteLine("Очистка Person...");
         Console.WriteLine("Очистка Person завершена...");
      }
   }

   public class Employee : Person
   {
      ~Employee()
      {
         Console.WriteLine("Очистка Employee...");
         object obj = null;
         // Следующий код сгенерирует исключение
         Console.WriteLine(obj.ToString());
         Console.WriteLine("Очистка Employee завершена...");
      }
   }
}
