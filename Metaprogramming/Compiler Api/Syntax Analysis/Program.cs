/**
 * Примеры синтаксического анализа
 */

using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MoreLinq;
using static System.Console;

namespace SyntaxAnalysis
{
   internal static class Program
   {
      const string Code = "class Foo { void Bar() {} }";

      static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(Code);
         var node = (CompilationUnitSyntax)tree.GetRoot();

         // Использование объектной модели
         node.Members.ForEach(codeMember =>
         {
            if (codeMember.Kind() == SyntaxKind.ClassDeclaration)
            {
               var @class = (ClassDeclarationSyntax)codeMember;
               @class.Members.ForEach(classMember =>
               {
                  if (classMember.Kind() == SyntaxKind.MethodDeclaration)
                  {
                     var method = (MethodDeclarationSyntax)classMember;
                     DoStuff(method);
                  }
               });
            }
         });

         // Использование LINQ
         var bars = from @class in node.Members.OfType<ClassDeclarationSyntax>()
                    from classMember in @class.Members.OfType<MethodDeclarationSyntax>()
                    where classMember.Identifier.Text == "Bar"
                    select classMember;
         var result = bars.ToList();
         result.ForEach(syntax => { WriteLine(syntax.Identifier.Value); });

         // Использование объектов-"посетителей"
         var visitor = new MyMethodVisitor();
         visitor.Visit(node);
      }

      internal static void DoStuff(MethodDeclarationSyntax method)
      {
         WriteLine(method.Identifier.Value);
      }
   }

   internal class MyMethodVisitor : CSharpSyntaxWalker
   {
      public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
      {
         Program.DoStuff(node);
         base.VisitMethodDeclaration(node);
      }
   }
}