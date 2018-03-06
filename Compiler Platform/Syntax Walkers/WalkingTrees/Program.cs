using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Console;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace WalkingTrees
{
   internal static class Program
   {
      private const string Code = @"
using System;

public class ContainsMethods
{
	public void Method1() { }
	public void Method2(int a, Guid b) { }
	public void Method3(string a) { }
	public void Method4(ref string a) { }
}";

      private static void Main()
      {
         var tree = ParseSyntaxTree(Code);
         WriteLine(tree.GetRoot().DescendantNodesAndTokensAndSelf(node => true, true).Count());

         PrintMethodContentViaTree(tree);
         WriteLine();

         PrintMethodContentViaWalker(tree);
         WriteLine();

         PrintMethodContentViaSemanticModel(tree);
      }

      private static void PrintMethodContentViaTree(SyntaxTree tree)
      {
         WriteLine(nameof(PrintMethodContentViaTree));
         var methods = tree.GetRoot().DescendantNodes(node => true).OfType<MethodDeclarationSyntax>();
         var methodSignatures = (from method in methods
                                 let parameters =
                                    method.ParameterList.Parameters.Select(
                                       parameter => $"{parameter.Type.ToFullString().Trim()} {parameter.Identifier.Text}").ToList()
                                 select $"{method.Identifier.Text}({string.Join(", ", parameters)})").ToList();
         methodSignatures.ForEach(WriteLine);
      }

      private static void PrintMethodContentViaWalker(SyntaxTree tree)
      {
         WriteLine(nameof(PrintMethodContentViaWalker));
         var methodWalker = new MethodWalker();
         methodWalker.Visit(tree.GetRoot());
      }

      private static void PrintMethodContentViaSemanticModel(SyntaxTree tree)
      {
         WriteLine(nameof(PrintMethodContentViaSemanticModel));
         var compilation = CSharpCompilation.Create(
            "MethodContent", new[] { tree }, new[]
            {
               MetadataReference.CreateFromFile(typeof (object).Assembly.Location)
            });
         var model = compilation.GetSemanticModel(tree, true);
         var methods = tree.GetRoot().DescendantNodes(node => true).OfType<MethodDeclarationSyntax>();
         var signatures = (from method in methods
                           select model.GetDeclaredSymbol(method)
            into methodSymbol
                           let parameters = (from parameter in methodSymbol.Parameters
                                             let isRef = parameter.RefKind == RefKind.Ref ? "ref " : string.Empty
                                             select $"{isRef}{parameter.Type.Name} {parameter.Name}").ToList()
                           select $"{methodSymbol.Name}({string.Join(", ", parameters)})").ToList();

         signatures.ForEach(WriteLine);
      }
   }
}