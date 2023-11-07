using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WorkingWithSymbols
{
   internal static class Program
   {
      const string Code = @"
using System;

class Foo
{
    void Bar(int x)
    {
        // #insideBar
    }
}

class Qux
{
    protected int Baz { get; set; }
}
";

      static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(Code);
         var root = tree.GetRoot();

         // Получим семантическую модель
         var compilation =
            CSharpCompilation.Create("Demo")
               .AddSyntaxTrees(tree)
               .AddReferences(MetadataReference.CreateFromFile(typeof (object).Assembly.Location));
         var model = compilation.GetSemanticModel(tree);

         // Обход включенной иерархии символов
         var cursor = Code.IndexOf("#insideBar", StringComparison.Ordinal);
         var barSymbol = model.GetEnclosingSymbol(cursor);
         for (var symbol = barSymbol; symbol != null; symbol = symbol.ContainingSymbol)
         {
            Console.WriteLine(symbol);
         }

         // Анализируем доступ Baz внутри Bar
         var bazProp =
            ((CompilationUnitSyntax) root).Members.OfType<ClassDeclarationSyntax>()
               .Single(classDecl => classDecl.Identifier.Text == "Qux")
               .Members.OfType<PropertyDeclarationSyntax>()
               .Single();
         var bazSymbol = model.GetDeclaredSymbol(bazProp);
         var canAccess = model.IsAccessible(cursor, bazSymbol);
         Console.WriteLine("Can access: {0}", canAccess ? "Yes" : "No");
      }
   }
}