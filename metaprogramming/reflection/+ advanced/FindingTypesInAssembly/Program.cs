// Нахождение типов в сборке

using System;
using System.Reflection;

namespace FindingTypesInAssembly
{
   internal static class Program
   {
      private static void Main()
      {
         const string dataAssembly = "System.Data, version=4.0.0.0, culture=neutral, PublicKeyToken=B77A5C561934E089";
         LoadAssemblyAndShowPublicTypes(dataAssembly);
      }

      private static void LoadAssemblyAndShowPublicTypes(string dataAssembly)
      {
         // Явно загружаем сборку в домен приложений
         var loadedAssembly = Assembly.Load(dataAssembly);

         // Выполняем цикл для каждого открытого типа, экспортируемого загруженной сборкой
         foreach (var exportedType in loadedAssembly.GetExportedTypes())
         {
            Console.WriteLine(exportedType.FullName);
         }
      }
   }
}