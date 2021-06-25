/**
 * Robust symbol renaming example
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

// ReSharper disable AsyncConverter.AsyncMethodNamingHighlighting

namespace RenamingSymbol
{
   internal static class Program
   {
      private static async Task Main()
      {
         var tree = CSharpSyntaxTree.ParseText(@"class Program
{
  static Program() {}
  public Program() {}
  static void Main()
  {
    Program p = new Program();
    p.Foo();
  }
  void Foo() => Bar();
  void Bar() => Foo();   
}
");

         var compilation = CSharpCompilation.Create("test")
            .AddReferences(MetadataReference.CreateFromFile(typeof(int).Assembly.Location))
            .AddSyntaxTrees(tree);

         var model = compilation.GetSemanticModel(tree);
         var tokens = (await tree.GetRootAsync().ConfigureAwait(false)).DescendantTokens().ToArray();

         // Rename the Program class to Program2:
         var program = tokens.First(token => token.Text == "Program");
         Console.WriteLine(RenameSymbol(model, program, "Program2").ToString());

         // Rename the Foo method to Foo2:
         var foo = tokens.Last(token => token.Text == "Foo");
         Console.WriteLine(RenameSymbol(model, foo, "Foo2").ToString());

         // Rename the p local variable to p2
         var p = tokens.Last(token => token.Text == "p");
         Console.WriteLine(RenameSymbol(model, p, "p2").ToString());
      }

      private static SyntaxTree RenameSymbol(SemanticModel model, SyntaxToken token, string newName)
      {
         var renameSpans = GetRenameSpans(model, token);
         IComparer<TextChange> comparer = new TextChangeComparerImpl();
         var newSourceText = model.SyntaxTree.GetText().WithChanges(
            renameSpans
               .Select(span => new TextChange(span, newName))
               .OrderBy(change => change, comparer));

         return model.SyntaxTree.WithChangedText(newSourceText);
      }

      private static IEnumerable<TextSpan> GetRenameSpans(SemanticModel model, SyntaxToken token)
      {
         var node = token.Parent;
         var symbol = model.GetSymbolInfo(node).Symbol ?? model.GetDeclaredSymbol(node);
         if (symbol == null)
         {
            return null; // No symbol to rename
         }

         var definitions =
            from location in symbol.Locations
            where location.SourceTree == node.SyntaxTree
            select location.SourceSpan;

         var usages =
            from descToken in model.SyntaxTree.GetRoot().DescendantTokens()
            where descToken.Text == symbol.Name
            let lSymbol = model.GetSymbolInfo(descToken.Parent).Symbol
            where lSymbol?.Equals(symbol) == true
            select descToken.Span;

         if (symbol.Kind != SymbolKind.NamedType)
         {
            return definitions.Concat(usages);
         }

         var structors =
            from type in model.SyntaxTree.GetRoot().DescendantNodes().OfType<TypeDeclarationSyntax>()
            where type.Identifier.Text == symbol.Name
            let declaredSymbol = model.GetDeclaredSymbol(type)
            where declaredSymbol.Equals(symbol)
            from method in type.Members
            let constructor = method as ConstructorDeclarationSyntax
            let destructor = method as DestructorDeclarationSyntax
            where constructor != null || destructor != null
            let identifier = constructor?.Identifier ?? destructor.Identifier
            select identifier.Span;

         return definitions.Concat(usages).Concat(structors);
      }

      private sealed class TextChangeComparerImpl : IComparer<TextChange>
      {
         public int Compare(TextChange x, TextChange y) => x.Span.Start - y.Span.Start;
      }
   }
}