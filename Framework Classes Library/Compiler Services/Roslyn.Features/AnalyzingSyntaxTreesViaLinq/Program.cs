using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AnalyzingSyntaxTreesViaLinq
{
   internal static class Program
   {
      private static readonly SyntaxTree SampleTree = CSharpSyntaxTree.ParseText(@"
public class MyClass
{
  public void MyMethod()
  {
  }
  public void MyMethod(int n)
  {
  }
}
");

      private static void Main()
      {
         var syntaxRoot = SampleTree.GetRoot();

         // Первый объявленный класс
         var myClass = syntaxRoot.DescendantNodes().OfType<ClassDeclarationSyntax>().First();

         // Первый объявленный метод
         var myMethod = syntaxRoot.DescendantNodes().OfType<MethodDeclarationSyntax>().First();

         // Метод, у которого есть параметры
         var myMethodInt =
            syntaxRoot.DescendantNodes()
               .OfType<MethodDeclarationSyntax>()
               .First(syntax => syntax.ParameterList.Parameters.Any());

         // Нахождение типа, которому принадлежит этот метод
         var containingType = myMethodInt.Ancestors().OfType<TypeDeclarationSyntax>().First();

         Console.WriteLine("The first class found:\t{0}", myClass.Identifier);
         Console.WriteLine("The first method found:\t{0}", myMethod.Identifier);
         Console.WriteLine("The parameterfull method:\t{0}", myMethodInt.Identifier);
         Console.WriteLine("Containing type:\t{0}", containingType.Identifier);
      }      
   }
}