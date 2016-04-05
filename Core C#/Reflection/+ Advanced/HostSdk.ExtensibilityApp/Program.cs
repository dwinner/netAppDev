// Хостинг подключаемых компонентов

using System;
using System.Linq;
using static System.Diagnostics.Debug;
using static System.IO.Path;
using static System.IO.Directory;
using static System.Reflection.Assembly;

namespace HostSdk.ExtensibilityApp
{
   internal static class Program
   {
      private static void Main()
      {
         // Определяем каталог, содержащий файл исполняемой сборки
         var addInDir = GetDirectoryName(GetEntryAssembly().Location);
         Assert(addInDir != null);

         // Предполагаем, что сборки подключаемых модулей находятся в одном каталоге с EXE-файлом хоста
         var addInAssemblies = GetFiles(addInDir, "*.dll");

         // Создаем набор типов, доступных для использования подключаемыми модулями
         var addInTypes = (from addInAssembly in addInAssemblies
                           select LoadFrom(addInAssembly)
                           into loadFromAssembly
                           // Загружаем сборки подключаемых модулей: выясняем, какие типы могут использоваться хостом
                           from exportedType in loadFromAssembly.GetExportedTypes() // Анализируем каждый экпортируемый открытый тип
                           where exportedType.IsClass && typeof(IAddIn).IsAssignableFrom(exportedType)
                           // Если тип представляет собой класс, реализующий интерфейс IAddIn, значит он доступен для использования хостом
                           select exportedType).ToList();

         // На данном этапе инициализация завершена: хост обнаружил все используемые подключаемые модули - создаем и используем объекты подключаемого модуля
         foreach (var addIn in addInTypes.Select(addInType => (IAddIn)Activator.CreateInstance(addInType)))
         {
            Console.WriteLine(addIn.DoSomething(5));
         }
      }
   }
}