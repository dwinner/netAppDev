/**
 * Создание кода средствами платформы компилятора
 */

using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CreatingTheSyntax
{
   internal static class Program
   {
      const string ClassFooVoidBar = "class Foo { void Bar() {} }";

      static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(ClassFooVoidBar);
         var node = tree.GetRoot();
         Console.WriteLine(node.ToString());

         var res =
            SyntaxFactory.ClassDeclaration("Foo")
               .WithMembers(
                  SyntaxFactory.List<MemberDeclarationSyntax>(new[]
                  {
                     SyntaxFactory.MethodDeclaration(
                        SyntaxFactory.PredefinedType(
                           SyntaxFactory.Token(SyntaxKind.VoidKeyword)), "Bar")
                        .WithBody(SyntaxFactory.Block())
                  }))
               .NormalizeWhitespace();

         Console.WriteLine(res);
      }
   }
}