// Посещение символов

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace SymbolVisitorSample
{
   internal static class Program
   {
      private static readonly SyntaxTree SourceTree = CSharpSyntaxTree.ParseText(@"
class MyClass
{
    class Nested
    {
    }
    void M()
    {
    }
}");

      private static void Main()
      {
         var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
         var compilation = CSharpCompilation.Create("MyCompilation", new[] { SourceTree }, new[] { mscorlib });

         //SymbolVisitor visitor = new NamedTypeVisitor();
         //visitor.Visit(compilation.GlobalNamespace);

         //SymbolVisitor visitor = new MethodSymbolVisitor();
         //visitor.Visit(compilation.GlobalNamespace);
      }
   }
}