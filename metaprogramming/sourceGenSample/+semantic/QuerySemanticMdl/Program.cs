using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace QuerySemanticMdl
{
   internal static class Program
   {
      private static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(@"class Program 
{
	static void Main() => System.Console.WriteLine (123);
}");

         var references = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES"))
            .Split(Path.PathSeparator)
            .Select(path => MetadataReference.CreateFromFile(path));

         var compilation = CSharpCompilation.Create("test")
            .AddReferences(references)
            .AddSyntaxTrees(tree);

         var model = compilation.GetSemanticModel(tree);

         var writeLineNode = tree.GetRoot().DescendantTokens().Single(
            t => t.Text == "WriteLine").Parent;

         var symbolInfo = model.GetSymbolInfo(writeLineNode);
         var symbol = symbolInfo.Symbol;
         Console.WriteLine(symbol.ToString()); // System.Console.WriteLine(int)
      }
   }
}