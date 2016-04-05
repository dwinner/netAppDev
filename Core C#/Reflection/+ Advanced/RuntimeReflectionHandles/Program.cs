// Использование описателей привязки для снижения потребления памяти процессором

using System.Linq;
using System.Reflection;
using static System.Console;
using static System.GC;
using static System.Reflection.BindingFlags;

namespace RuntimeReflectionHandles
{
   internal static class Program
   {
      private const BindingFlags DefaultBindingFlags = FlattenHierarchy | Instance | Static | Public | NonPublic;

      private static void Main()
      {
         // Выводим размер кучи до отражения
         Show("Before doing anything");

         // Создаем кэш объектов MethodInfo для всех методов из mscorlib.dll
         var methodInfos =
            typeof (object).Assembly.GetExportedTypes()
               .Where(exportedType => !exportedType.IsGenericTypeDefinition)
               .SelectMany(exportedType => exportedType.GetMethods(DefaultBindingFlags))
               .Cast<MethodBase>()
               .ToList();

         // Выводим число методов и размер кучи после привязки всех методов
         WriteLine("# of methods={0:N0}", methodInfos.Count);
         Show("After building cache of MethodInfo objects");

         // Создаем кэш описателей RuntimeMethodHandles для всех объектов MethodInfo
         var methodHandles = methodInfos.ConvertAll(input => input.MethodHandle);
         Show("Holding MethodInfo and RuntimeMethodHandle cache");
         KeepAlive(methodInfos); // Запрещаем сборку мусора в кэше

         methodInfos = null; // Разрешаем сборку мусора в кэше
         Show("After freeing MethodInfo objects");

         methodInfos = methodHandles.ConvertAll(MethodBase.GetMethodFromHandle);
         Show("Size of heap after re-creating MethodInfo objects");
         KeepAlive(methodHandles);
         KeepAlive(methodInfos);

         methodHandles = null;
         methodInfos = null;
         Show("After freeing MethodInfos and RuntimeMethodHandles");
      }

      private static void Show(string s) => WriteLine("Heap size={0,12:N0} - {1}", GetTotalMemory(true), s);
   }
}