/**
 * Шаблон проектирования Visitor.
 * Реализация через методы расширения.
 */

using System;

namespace _06_GoffVisitorViaExtension
{
   class Program
   {
      static void Main()
      {
         const string data = "некоторые важные данные";
         var supplies = new SupplyCabinet();
         var hrLady = new Employee();
         data.Validate();
         supplies.Validate(); // Принудительно вызвать обобщенную версию: supplies.Validate<SupplyCabinet>();
         hrLady.Validate();

         Console.ReadKey();
      }
   }

   public interface IValidator
   {
      void DoValidation();
   }

   public class Employee
   {

   }

   public class SupplyCabinet : IValidator
   {
      public void DoValidation()
      {
         Console.WriteLine("\tПроверка SupplyCabinet");
      }
   }

   public static class Validators
   {
      public static void Validate(this string str)
      {
         // Что-то делать для проверки экземпляра string
         Console.WriteLine("Экземпляр string с \"{0}\" проверен", str);
      }

      public static void Validate(this Employee employee)
      {
         // Что-то делать для проверки экземпляра Employee
         Console.WriteLine("Сбой при проверке экземпляра типа Employee");
      }

      public static void Validate<T>(this T obj)
         where T : IValidator
      {
         obj.DoValidation();
         Console.WriteLine("Проверен экземпляр следующего типа: {0}", obj.GetType());
      }
   }
}
