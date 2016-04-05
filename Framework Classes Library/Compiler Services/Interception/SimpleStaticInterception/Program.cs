/**
 * Статическое внедрение в сборку
 */

using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace SimpleStaticInterception
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         if (args.Length == 0)
         {
            return;
         }

         var assemblyPath = args[0];

         // Считываем сборку в формате Mono.Cecil
         var assembly = AssemblyDefinition.ReadAssembly(assemblyPath);

         // Получаем метод Console.WriteLine, используя стандартные методы отражения
         var writeLnMethod = typeof(Console).GetMethod(nameof(Console.WriteLine), new[] { typeof(string) });

         // Создаем ссылку на метод, полученный отражением, для использования в Mono.Cecil
         var writeLnRef = assembly.MainModule.Import(writeLnMethod);
         foreach (var method in assembly.MainModule.Types.SelectMany(typeDef => typeDef.Methods))
         {
            // Для каждого метода в полученной сборке загружаем на стек строку "Inject!"
            method.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Ldstr, "Inject!"));

            // Вызываем метод Console.WriteLine, параметры он берет из стека - в данном случае строку "Injected".
            method.Body.Instructions.Insert(1, Instruction.Create(OpCodes.Call, writeLnRef));
         }

         assembly.Write(assemblyPath);
      }
   }
}