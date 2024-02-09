// Нахождение производных типов во время выполнения

using System;
using System.Linq;
using System.Reflection;
using static System.Console;

namespace FindingDerivedTypes
{
   internal static class Program
   {
      private static void Main()
      {
         LoadAssemblies(); // Явная загрузка сборок для отражения

         // Рекурсивная сборка иерархии класса как строки, разделенной дефисами
         Func<Type, string> classNameAndBase = null;
         classNameAndBase = type =>
            $"-{type.FullName}{((type.BaseType != typeof(object)) ? classNameAndBase(type.BaseType) : string.Empty)}";

         // Определение запроса для нахождения всех открытых типов, унаследованных от Exception в данной сборке домена приложений
         var exceptionTree = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                              from exportedType in assembly.GetExportedTypes()
                              where exportedType.IsClass && exportedType.IsPublic && typeof(Exception).IsAssignableFrom(exportedType)
                              let typeHierarchyTemp = classNameAndBase(exportedType).Split('-').Reverse().ToArray()
                              let typeHierarchy = string.Join("-", typeHierarchyTemp, 0, typeHierarchyTemp.Length - 1)
                              orderby typeHierarchy
                              select typeHierarchy).ToArray();

         // Вывод дерева исключений
         WriteLine("{0} Exception types found.", exceptionTree.Length);
         foreach (var x in exceptionTree.Select(exIer => exIer.Split('-'))/* Разделить базовые типы */)
         {
            // Изъять типы, основанные на номере базовых типов, и показать унаследованные типы
            WriteLine(new string(' ', 3 * (x.Length - 1)) + x[x.Length - 1]);
         }
      }

      private static void LoadAssemblies()
      {
         string[] assemblies =
         {
            "System,                    PublicKeyToken={0}",
            "System.Core,               PublicKeyToken={0}",
            "System.Data,               PublicKeyToken={0}",
            "System.Design,             PublicKeyToken={1}",
            "System.DirectoryServices,  PublicKeyToken={1}",
            "System.Drawing,            PublicKeyToken={1}",
            "System.Drawing.Design,     PublicKeyToken={1}",
            "System.Management,         PublicKeyToken={1}",
            "System.Messaging,          PublicKeyToken={1}",
            "System.Runtime.Remoting,   PublicKeyToken={0}",
            "System.Security,           PublicKeyToken={1}",
            "System.ServiceProcess,     PublicKeyToken={1}",
            "System.Web,                PublicKeyToken={1}",
            "System.Web.RegularExpressions, PublicKeyToken={1}",
            "System.Web.Services,       PublicKeyToken={1}",
            "System.Windows.Forms,      PublicKeyToken={0}",
            "System.Xml,                PublicKeyToken={0}"
         };

         const string ecmaPublicKeyToken = "b77a5c561934e089";
         const string msPublicKeyToken = "b03f5f7f11d50a3a";

         // Получение версии сборки, содержащей System.Object. Мы принимаем одну версию для всех других сборок
         var objectVersion = typeof(object).Assembly.GetName().Version;

         // Явная загрузка сборок, которые мы хотим отразить
         foreach (
            var assemblyIdentity in
               assemblies.Select(
                  assembly =>
                     $"{string.Format(assembly, ecmaPublicKeyToken, msPublicKeyToken)}, Culture=neutral, Version={objectVersion}")
            )
         {
            Assembly.Load(assemblyIdentity);
         }
      }
   }
}