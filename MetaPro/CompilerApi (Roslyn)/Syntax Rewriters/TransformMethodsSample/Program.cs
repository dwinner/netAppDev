using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TransformMethodsSample
{
   internal static class Program
   {
      private static void Main()
      {
         var code = File.ReadAllText("Sample.cs");
         TransformMethodToUppercaseAsync(code).Wait();
      }

      private static async Task TransformMethodToUppercaseAsync(string code)
      {
         var tree = CSharpSyntaxTree.ParseText(code);
         var root = await tree.GetRootAsync().ConfigureAwait(false);

         var methods =
            root.DescendantNodes()
               .OfType<MethodDeclarationSyntax>()
               .Where(m => char.IsLower(m.Identifier.ValueText.First()))
               .Where(m => m.Modifiers.Select(t => t.Value).Contains("public"))
               .ToList();
         root = root.ReplaceNodes(methods, (oldMethod, newMethod) =>
         {
            var newName =
               char.ToUpperInvariant(oldMethod.Identifier.ValueText[0]) + oldMethod.Identifier.ValueText.Substring(1);
            return newMethod.WithIdentifier(SyntaxFactory.Identifier(newName));
         });

         Console.WriteLine();
         Console.WriteLine(root.ToString());
      }
   }
}