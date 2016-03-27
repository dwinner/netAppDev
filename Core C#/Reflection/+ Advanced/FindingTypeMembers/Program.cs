// Нахождение членов типа

using System;
using System.Reflection;
using static System.Reflection.BindingFlags;
using static System.String;

namespace FindingTypeMembers
{
   internal static class Program
   {
      private static void Main()
      {
         // В цикле перечисляем все сборки, загруженные в данный домен приложений
         var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
         foreach (var assembly in loadedAssemblies)
         {
            FormatWrite(0, "Assembly: {0}", assembly);

            // Перечисляем типы сборки
            foreach (var type in assembly.GetExportedTypes())
            {
               FormatWrite(1, "Type: {0}", type);

               // Находим члены типа
               const BindingFlags bindingFlags = DeclaredOnly | NonPublic | Public | Instance | Static;
               foreach (var member in type.GetMembers(bindingFlags))
               {
                  var typeName = Empty;
                  if (member is Type)
                  {
                     typeName = "(Nested) Type";
                  }
                  else if (member is FieldInfo)
                  {
                     typeName = nameof(FieldInfo);
                  }
                  else if (member is MethodInfo)
                  {
                     typeName = nameof(MethodInfo);
                  }
                  else if (member is ConstructorInfo)
                  {
                     typeName = nameof(ConstructorInfo);
                  }
                  else if (member is PropertyInfo)
                  {
                     typeName = nameof(PropertyInfo);
                  }
                  else if (member is EventInfo)
                  {
                     typeName = nameof(EventInfo);
                  }

                  FormatWrite(2, "{0}: {1}", typeName, member);
               }
            }
         }
      }

      private static void FormatWrite(int indent, string format, params object[] args)
      {
         Console.WriteLine(new string(' ', 3 * indent) + format, args);
      }
   }
}