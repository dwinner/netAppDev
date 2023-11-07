using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GetConstantValueSample
{
   internal static class Program
   {
      const string Code = @"
using System;

class Foo
{
    void Bar(int x)
    {
        Console.WriteLine(3.14);
        Console.WriteLine(""qux"");
        Console.WriteLine('c');
        Console.WriteLine(null);
        Console.WriteLine(x * 2 + 1);
    }
}
";

      static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(Code);
         var root = tree.GetRoot();

         var compilation =
            CSharpCompilation.Create("Demo")
               .AddSyntaxTrees(tree)
               .AddReferences(MetadataReference.CreateFromFile(typeof (object).Assembly.Location));
         var model = compilation.GetSemanticModel(tree);

         var walker = new ConsoleWriteLineWalker();
         walker.Visit(root);

         // јнализируем аргументы с посто€нными значени€ми
         walker.Arguments.ForEach(arg =>
         {
            var val = model.GetConstantValue(arg);
            Console.WriteLine(val.HasValue
               ? $"{arg} has constant value {val.Value ?? "nul"} of type {val.Value?.GetType() ?? typeof (object)}"
               : $"{arg} has no constant value");
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