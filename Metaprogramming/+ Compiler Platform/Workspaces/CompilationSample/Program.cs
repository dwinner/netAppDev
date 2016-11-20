using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static System.Console;

namespace CompilationSample
{
   internal static class Program
   {
      const string Code = "class Foo { void Bar(int x) {} }";

      static void Main()
      {         
         var tree = CSharpSyntaxTree.ParseText(Code);
         var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
         var references = new MetadataReference[]
         { MetadataReference.CreateFromFile(typeof (object).Assembly.Location) };
         var trees = new[] { tree };
         var compilation =
            CSharpCompilation.Create("Demo")
               .AddSyntaxTrees(trees)
               .AddReferences(references)
               .WithOptions(compilationOptions);
         var result = compilation.Emit("Demo.dll");
         if (!result.Success)
         {
            foreach (var diagnostic in result.Diagnostics)
            {
               WriteLine(diagnostic.GetMessage());
            }
         }
      }
   }
}