// Работа с семантической моделью

using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SemanticModelSample
{
   internal static class Program
   {
      private static readonly SyntaxTree _SourceTree = CSharpSyntaxTree.ParseText(@"
public class MyClass {
   int Method1() { return 0; }
	void Method2()
	{
	   int x = Method1();
	}
}
}");

      private static void Main()
      {
         // Получение семантической модели из объекта компиляции
         var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
         var compilation = CSharpCompilation.Create("MyCompilation", new[] { _SourceTree }, new[] { mscorlib });
         var model = compilation.GetSemanticModel(_SourceTree);

         // Исследование объявленных символов модели
         var methodSyntax = _SourceTree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().First();
         var methodSymbol = model.GetDeclaredSymbol(methodSyntax);

         Console.WriteLine(methodSymbol.ToString()); // MyClass.Method1()
         Console.WriteLine(methodSymbol.ContainingSymbol); // MyClass
         Console.WriteLine(methodSymbol.IsAbstract); // false

         // Исследование выражений в поисках символов модели
         var invocationSyntax = _SourceTree.GetRoot().DescendantNodes().OfType<InvocationExpressionSyntax>().First();
         var invokedSymbol = model.GetSymbolInfo(invocationSyntax).Symbol;

         Console.WriteLine(invokedSymbol.ToString()); // MyClass.Method1()
         Console.WriteLine(invokedSymbol.ContainingSymbol); // MyClass
         Console.WriteLine(invokedSymbol.IsAbstract); // false

         Console.WriteLine(invokedSymbol.Equals(methodSymbol)); // true
      }
   }
}