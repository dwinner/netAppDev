/**
 * Запуск диагностик
 */

using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace RunningDiagnostics
{
   internal static class Program
   {
      private const string Code = @"
class Person
{
    private int _age;
}";

      private static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(Code);
         var compilation =
            CSharpCompilation.Create("Demo")
               .AddSyntaxTrees(tree)
               .AddReferences(MetadataReference.CreateFromFile(typeof (object).Assembly.Location))
               .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

         // Получим все диагностики
         var diagnostics = compilation.GetDiagnostics();
         foreach (var diagnostic in diagnostics)
         {
            Console.WriteLine(diagnostic);
         }
      }
   }
}