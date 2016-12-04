// Реализация собственной логики обхода

using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CustomWalkers
{
   internal static class Program
   {
      private static readonly SyntaxTree SourceTree = CSharpSyntaxTree.ParseText(@"
public class MyClass
{
   public void MyMethod()
   {
   }
   public void MyMethod(int n)
   {
   }
");

      private static void Main()
      {
         CSharpSyntaxWalker customWalker = new CustomWalker();
         customWalker.Visit(SourceTree.GetRoot());

         Console.WriteLine("-----------------------------------");

         CSharpSyntaxWalker cmWalker = new ClassMethodWalker();
         cmWalker.Visit(SourceTree.GetRoot());
      }
   }

   public class ClassMethodWalker : CSharpSyntaxWalker
   {
      private string _className = string.Empty;

      public override void VisitClassDeclaration(ClassDeclarationSyntax node)
      {
         _className = node.Identifier.ToString();
         base.VisitClassDeclaration(node);
      }

      public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
      {
         var methodName = node.Identifier.ToString();
         Console.WriteLine("{0}.{1}", _className, methodName);
         base.VisitMethodDeclaration(node);
      }
   }
}