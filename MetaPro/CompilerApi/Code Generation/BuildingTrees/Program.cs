/**
 * Building trees for further compilation
 */

using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Console;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace BuildingTrees
{
   internal static class Program
   {
      private const string NamespaceName = "BuildingTrees";
      private const string ClassName = "Doubler";
      private const string MethodName = "Double";
      private const string DllName = "Doubler.dll";

      private static void Main()
      {
         const string paramName = "a";

         var unit = CompilationUnit()
            .WithMembers(
               SingletonList<MemberDeclarationSyntax>(
                  NamespaceDeclaration(
                     IdentifierName(NamespaceName))
                     .WithMembers(
                        SingletonList<MemberDeclarationSyntax>(
                           ClassDeclaration(ClassName)
                              .WithModifiers(
                                 TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)))
                              .WithMembers(
                                 SingletonList<MemberDeclarationSyntax>(
                                    MethodDeclaration(
                                       PredefinedType(
                                          Token(SyntaxKind.IntKeyword)),
                                       Identifier(MethodName))
                                       .WithModifiers(
                                          TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)))
                                       .WithParameterList(
                                          ParameterList(
                                             SingletonSeparatedList(
                                                Parameter(
                                                   Identifier(paramName))
                                                   .WithType(
                                                      PredefinedType(
                                                         Token(SyntaxKind.IntKeyword))))))
                                       .WithBody(
                                          Block(
                                             SingletonList<StatementSyntax>(
                                                ReturnStatement(
                                                   BinaryExpression(
                                                      SyntaxKind.MultiplyExpression,
                                                      LiteralExpression(
                                                         SyntaxKind.NumericLiteralExpression,
                                                         Literal(2)),
                                                      IdentifierName(paramName)))))))))))).NormalizeWhitespace();
         var tree = SyntaxTree(unit);
         var compilation = CSharpCompilation.Create(DllName,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
            syntaxTrees: new[] {tree},
            references: new[] {MetadataReference.CreateFromFile(typeof (object).Assembly.Location)});
         using (var stream = new MemoryStream())
         {
            var compileResult = compilation.Emit(stream);
            WriteLine("Compile result: {0}", compileResult.Success ? "Ok" : "Fail");

            var assembly = Assembly.Load(stream.GetBuffer());

            var type = assembly.GetType($"{NamespaceName}.{ClassName}");
            var method = type.GetMethod(MethodName);
            var invokeResult = (int) method.Invoke(null, new object[] {2});

            WriteLine(invokeResult);
         }
      }
   }
}