// Редактирование/обновление узлов в существующем документе

using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace DocumentEditingSample
{
   internal static class Program
   {
      private static readonly SourceText SourceText = SourceText.From(@"
class C
{
    void M()
    {
        char key = Console.ReadKey();
        if (key == 'A')
        {
            Console.WriteLine(""You pressed A"");
        }
        else
        {
            Console.WriteLine(""You didn't press A"");
        }
    }
}");

      private static void Main()
      {
         //var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

         // Получаем рабочее пространство для манипуляций
         var workspace = new AdhocWorkspace();
         var projectId = ProjectId.CreateNewId();
         var versionStamp = VersionStamp.Create();
         var projectInfo = ProjectInfo.Create(projectId, versionStamp, "NewProject", "projName", LanguageNames.CSharp);
         var newProject = workspace.AddProject(projectInfo);
         var document = workspace.AddDocument(newProject.Id, "NewFile.cs", SourceText);
         var syntaxRoot = document.GetSyntaxRootAsync().Result;
         var ifStatement = syntaxRoot.DescendantNodes().OfType<IfStatementSyntax>().Single();

         // Создаем узлы для выражений вызовов метода
         var conditionWasTrueInvocation = CreateParameterlessInvocation("LogConditionWasTrue");
         var conditionWasFalseInvocation = CreateParameterlessInvocation("LogConditionWasFalse");

         // Изменяем документ, вставляя в него новые узлы
         var documentEditor = DocumentEditor.CreateAsync(document).Result;
         documentEditor.InsertBefore(ifStatement.Statement.ChildNodes().Single(), conditionWasTrueInvocation);
         documentEditor.InsertAfter(ifStatement.Else.Statement.ChildNodes().Single(), conditionWasFalseInvocation);

         var newDocument = documentEditor.GetChangedDocument();
         Console.WriteLine(newDocument.GetSyntaxRootAsync().Result.ToString());
      }

      private static ExpressionStatementSyntax CreateParameterlessInvocation(string methodName)
         =>
            ExpressionStatement(
               InvocationExpression(IdentifierName(methodName))
                  .WithArgumentList(
                     ArgumentList()
                        .WithOpenParenToken(Token(SyntaxKind.OpenParenToken))
                        .WithCloseParenToken(Token(SyntaxKind.CloseParenToken))))
               .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
   }
}