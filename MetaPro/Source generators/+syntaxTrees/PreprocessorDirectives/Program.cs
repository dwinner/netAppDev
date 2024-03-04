using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PreprocessorDirectives
{
   internal static class Program
   {
      private static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(@"#define FOO");
         Console.WriteLine(tree);
         var root = tree.GetRoot();
         var trivia = root.DescendantTrivia().First();
         Console.WriteLine(trivia.HasStructure); // True
         Console.WriteLine(trivia.GetStructure().Kind()); // DefineDirectiveTrivia
         Console.WriteLine(root.ContainsDirectives); // True

         // directive is the root node of the structured trivia:
         var directive = root.GetFirstDirective();
         Console.WriteLine(directive.Kind()); // DefineDirectiveTrivia
         Console.WriteLine(directive.ToString()); // #define FOO

         // If there were more directives, we could get to them as follows:
         Console.WriteLine(directive.GetNextDirective()); // (null)

         var hashDefine = (DefineDirectiveTriviaSyntax)root.GetFirstDirective();
         Console.WriteLine(hashDefine.Name.Text); // FOO
      }
   }
}