/*
 * Сделано в SharpDevelop.
 * Пользователь: Denis
 * Дата: 21.01.2013
 * Время: 23:29
 *  
 */
using System;
using System.Reflection;
using System.Linq;

namespace MyTypeViewer
{
   class Program
   {
      public static void Main(string[] args)
      {
         Console.WriteLine("***** Welcome to MyTypeViewer *****");
         string typeName = "";
         
         do
         {
            Console.WriteLine("\nEnter a type name to evaluate"); // Запрос имени типа.
            Console.WriteLine("Or Enter Q to quit: ");   // Для выхода ввести Q.
            
            // Получение имени типа.
            typeName = Console.ReadLine();
            if (typeName.ToUpper() == "Q")
               break;
            // Отобразить информацию о типе.
            try
            {
               Type type = Type.GetType(typeName);
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
         }
         while (true);
         
         Console.Write("Press any key to continue . . . ");
         Console.ReadKey(true);
      }
      
      // Отображение имен методов типа.
      static void ListMethods(Type type)
      {
         Console.WriteLine("***** Methods *****");
         /*var methodNames = from n in type.GetMethods() select n.Name;
         foreach (var name in methodNames)
            Console.WriteLine("->{0}", name);
         Console.WriteLine();*/
         MethodInfo[] methodInfoArray = type.GetMethods();
         foreach (MethodInfo methodInfo in methodInfoArray)
         {
            // Получение информации о возвращаемом типе.
            string retVal = methodInfo.ReturnType.FullName;
            string paramInfo = "{ ";
            // Получение информации о принимаемых параметрах.
            foreach (ParameterInfo lParamInfo in methodInfo.GetParameters())
            {
               paramInfo += string.Format("{0} {1} ",
                                         lParamInfo.ParameterType,
                                         lParamInfo.Name);
            }
            paramInfo += " )";
            // Отображение общей сигнатуры метода.
            Console.WriteLine("->{0} {1} {2}", retVal, methodInfo.Name, paramInfo);
         }
         Console.WriteLine();
      }
      
      // Отображение имен полей типа.
      static void ListFields(Type type)
      {
         Console.WriteLine("***** Fields *****");         
         var fieldNames = from f in type.GetFields() select f.Name;
         foreach (var name in fieldNames)
            Console.WriteLine("->{0}", name);
         Console.WriteLine();
      }
      
      // Отображение имен свойств типа.
      static void ListProps(Type type)
      {
         Console.WriteLine("***** Properties *****");
         var propNames = from p in type.GetProperties() select p.Name;
         foreach (var name in propNames)
            Console.WriteLine("->{0}", name);
         Console.WriteLine();
      }
      
      // Отображение имен реализуемых интерфейсов.
      static void ListInterfaces(Type type)
      {
         Console.WriteLine("***** Interfaces *****");
         var ifaces = from i in type.GetInterfaces() select i;
         foreach (Type i in ifaces)
            Console.WriteLine("->{0}", i.Name);
      }
      
      // Отображение для предоставления полной картины.
      static void ListVariousStats(Type type)
      {
         Console.WriteLine("***** Various Statistics *****");
         Console.WriteLine("Base class is: {0}", type.BaseType);  // Базовый класс.
         Console.WriteLine("Is type abstract? {0}", type.IsAbstract);   // Абстрактный?
         Console.WriteLine("Is type sealed? {0}", type.IsSealed); // Запечатанный?
         Console.WriteLine("Is type generic {0}", type.IsGenericTypeDefinition); // Обобщенный?
         Console.WriteLine("Is type a class type? {0}", type.IsClass);  // Класс?
         Console.WriteLine();
      }
   }
}