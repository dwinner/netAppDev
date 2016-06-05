/**
 * ѕерезапись дерева на автоматические свойства
 */

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AutoPropertyRewriterSample
{
   internal static class Program
   {
      private static void Main()
      {
         var code = File.ReadAllText("Sample.cs");
         ProcessAsync(code).Wait();
      }

      private static async Task ProcessAsync(string code)
      {
         var tree = CSharpSyntaxTree.ParseText(code);
         var compilation =
            CSharpCompilation.Create("Sample")
               .AddReferences(MetadataReference.CreateFromFile(typeof (object).Assembly.Location))
               .AddSyntaxTrees(tree);
         var semanticModel = compilation.GetSemanticModel(tree);

         var propertyRewriter = new AutoPropertyRewriter(semanticModel);
         var root = await tree.GetRootAsync().ConfigureAwait(false);
         var rootWithAutoProperties = propertyRewriter.Visit(root);

         compilation = compilation.RemoveAllSyntaxTrees().AddSyntaxTrees(rootWithAutoProperties.SyntaxTree);
         /*semanticModel = */compilation.GetSemanticModel(rootWithAutoProperties.SyntaxTree);

         var fieldRewriter = new RemoveBackingFieldRewriter(propertyRewriter.FieldsToRemove.ToArray());
         var rootWithFieldsRemoved = fieldRewriter.Visit(rootWithAutoProperties);
         Console.WriteLine(rootWithFieldsRemoved);
      }
   }
}