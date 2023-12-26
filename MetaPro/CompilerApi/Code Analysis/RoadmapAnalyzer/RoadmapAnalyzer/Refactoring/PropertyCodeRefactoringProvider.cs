using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoadmapAnalyzer.Refactoring
{
   [ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(PropertyCodeRefactoringProvider)), Shared]
   internal class PropertyCodeRefactoringProvider : CodeRefactoringProvider
   {
      public sealed override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
      {
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
         var selectedNode = root.FindNode(context.Span);
         var propertyDecl = selectedNode as PropertyDeclarationSyntax;
         if (propertyDecl == null || !IsAutoImplementedProperty(propertyDecl))
         {
            return;
         }

         var action = CodeAction.Create("Apply full property",
            token => ChangeToFullPropertyAsync(context.Document, propertyDecl, token));
         context.RegisterRefactoring(action);
      }

      private static bool IsAutoImplementedProperty(BasePropertyDeclarationSyntax propertyDecl)
      {
         var accessors = propertyDecl.AccessorList.Accessors;
         var getter = accessors.FirstOrDefault(ad => ad.Kind() == SyntaxKind.GetAccessorDeclaration);
         var setter = accessors.FirstOrDefault(ad => ad.Kind() == SyntaxKind.SetAccessorDeclaration);

         return getter != null && setter != null && (getter.Body == null || setter.Body == null);
      }

      private static async Task<Document> ChangeToFullPropertyAsync(Document document, PropertyDeclarationSyntax propertyDecl,
         CancellationToken cancellationToken)
      {
         var model = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
         var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false) as CompilationUnitSyntax;
         document =
            document.WithSyntaxRoot(CodeGeneration.ImplementFullProperty(root, model, propertyDecl,
               document.Project.Solution.Workspace));

         return document;
      }
   }
}