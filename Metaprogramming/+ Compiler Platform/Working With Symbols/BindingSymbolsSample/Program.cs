using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Console;

namespace BindingSymbolsSample
{
   internal static class Program
   {
      const string Code = @"
using System;

class Foo
{
    private int y;

    void Bar(int x)
    {
        Console.WriteLine(x);
        Console.WriteLine(y);

        int z = 42;
        Console.WriteLine(z);

        Console.WriteLine(a);
    }
}";

      static void Main()
      {
         // Получаем дерево компиляции
         var tree = CSharpSyntaxTree.ParseText(Code);
         var root = tree.GetRoot();

         // Получаем семантическую модель
         MetadataReference[] references = { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) };
         var compilation = CSharpCompilation.Create("Demo").AddSyntaxTrees(tree).AddReferences(references);
         var model = compilation.GetSemanticModel(tree);

         // Обходим дерево
         var walker = new ConsoleWriteLineWalker();
         walker.Visit(root);

         // Привязываем аргументы
         walker.Arguments.ForEach(arg =>
         {
            var symbolInfo = model.GetSymbolInfo(arg);
            if (symbolInfo.Symbol != null)
            {
               WriteLine("{0} is bound to {1} of type {2}", arg, symbolInfo.Symbol, symbolInfo.Symbol.Kind);
            }
            else
            {
               WriteLine("{0} could not be bound", arg);
            }
         });
      }
   }

   internal class ConsoleWriteLineWalker : CSharpSyntaxWalker
   {
      public ConsoleWriteLineWalker()
      {
         Arguments = new List<ExpressionSyntax>();
      }

      public List<ExpressionSyntax> Arguments { get; }

      public override void VisitInvocationExpression(InvocationExpressionSyntax node)
      {
         var member = node.Expression as MemberAccessExpressionSyntax;
         var type = member?.Expression as IdentifierNameSyntax;
         if (type != null && type.Identifier.Text == nameof(Console) &&
             member.Name.Identifier.Text == nameof(Console.WriteLine) && node.ArgumentList.Arguments.Count == 1)
         {
            var arg = node.ArgumentList.Arguments.Single().Expression;
            Arguments.Add(arg);
            return;
         }

         base.VisitInvocationExpression(node);
      }
   }
}