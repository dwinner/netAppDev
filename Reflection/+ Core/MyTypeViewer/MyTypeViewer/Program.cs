/*
 * Сделано в SharpDevelop.
 * Пользователь: Denis
 * Дата: 21.01.2013
 * Время: 23:29
 *  
 */

using System;
using System.Linq;

namespace MyTypeViewer
{
   internal static class Program
   {
      public static void Main()
      {
         Console.WriteLine("***** Welcome to MyTypeViewer *****");

         do
         {
            Console.WriteLine("\nEnter a type name to evaluate"); // Запрос имени типа.
            Console.WriteLine("Or Enter Q to quit: "); // Для выхода ввести Q.

            // Получение имени типа.
            var typeName = Console.ReadLine();
            if (typeName.ToUpper() == "Q")
               break;
            // Отобразить информацию о типе.
            try
            {
               var type = Type.GetType(typeName);
               Console.WriteLine();
               ListVariousStats(type);
               ListFields(type);
               ListProps(type);
               ListMethods(type);
               ListInterfaces(type);
            }
            catch // Указанный тип не найден
            {
               Console.WriteLine("Sorry, can't find type");
            }
         } while (true);

         Console.Write("Press any key to continue . . . ");
         Console.ReadKey(true);
      }

      // Отображение имен методов типа.
      private static void ListMethods(Type type)
      {
         Console.WriteLine("***** Methods *****");
         /*var methodNames = from n in type.GetMethods() select n.Name;
         foreach (var name in methodNames)
            Console.WriteLine("->{0}", name);
         Console.WriteLine();*/
         var methodInfoArray = type.GetMethods();
         foreach (var methodInfo in methodInfoArray)
         {
            // Получение информации о возвращаемом типе.
            var retVal = methodInfo.ReturnType.FullName;
            var paramInfo = "{ ";
            // Получение информации о принимаемых параметрах.
            foreach (var lParamInfo in methodInfo.GetParameters())
               paramInfo += $"{lParamInfo.ParameterType} {lParamInfo.Name} ";
            paramInfo += " )";
            // Отображение общей сигнатуры метода.
            Console.WriteLine("->{0} {1} {2}", retVal, methodInfo.Name, paramInfo);
         }
         Console.WriteLine();
      }

      // Отображение имен полей типа.
      private static void ListFields(Type type)
      {
         Console.WriteLine("***** Fields *****");
         var fieldNames = from f in type.GetFields() select f.Name;
         foreach (var name in fieldNames)
            Console.WriteLine("->{0}", name);
         Console.WriteLine();
      }

      // Отображение имен свойств типа.
      private static void ListProps(Type type)
      {
         Console.WriteLine("***** Properties *****");
         var propNames = from p in type.GetProperties() select p.Name;
         foreach (var name in propNames)
            Console.WriteLine("->{0}", name);
         Console.WriteLine();
      }

      // Отображение имен реализуемых интерфейсов.
      private static void ListInterfaces(Type type)
      {
         Console.WriteLine("***** Interfaces *****");
         var ifaces = from i in type.GetInterfaces() select i;
         foreach (var i in ifaces)
            Console.WriteLine("->{0}", i.Name);
      }

      // Отображение для предоставления полной картины.
      private static void ListVariousStats(Type type)
      {
         Console.WriteLine("***** Various Statistics *****");
         Console.WriteLine("Base class is: {0}", type.BaseType); // Базовый класс.
         Console.WriteLine("Is type abstract? {0}", type.IsAbstract); // Абстрактный?
         Console.WriteLine("Is type sealed? {0}", type.IsSealed); // Запечатанный?
         Console.WriteLine("Is type generic {0}", type.IsGenericTypeDefinition); // Обобщенный?
         Console.WriteLine("Is type a class type? {0}", type.IsClass); // Класс?
         Console.WriteLine();
      }
   }
}