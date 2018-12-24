using System;
using System.Collections.Immutable;
using System.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExtractTypesToFiles.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ExtractTypesToFiles
{
   [ExportCodeRefactoringProvider(
      LanguageNames.CSharp, Name = nameof(ExtractTypesToFilesCodeRefactoringProvider)), Shared]
   public sealed class ExtractTypesToFilesCodeRefactoringProvider : CodeRefactoringProvider
   {
      private const string MoveByFolders = "Move types to files in folders";
      private const string MoveInCurrentFolder = "Move types to files in current folder";

      public override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
      {
         var document = context.Document;
         var documentFileNameWithoutExtension = Path.GetFileNameWithoutExtension(document.FilePath);
         var root = await document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
         var model = await document.GetSemanticModelAsync(context.CancellationToken).ConfigureAwait(false);
         var typesToRemove = root.GetTypesToRemove(model, documentFileNameWithoutExtension);
         if (typesToRemove.Length > 1)
         {
            context.RegisterRefactoring(CodeAction.Create(MoveByFolders, token =>
                  CreateFilesAsync(document, root, model, typesToRemove, _ => _.Replace(".", "\\"), token)));
            context.RegisterRefactoring(CodeAction.Create(MoveInCurrentFolder, token =>
                  CreateFilesAsync(document, root, model, typesToRemove, _ => string.Empty, token)));
         }
      }

      private static async Task<Solution> CreateFilesAsync(
         TextDocument document, SyntaxNode root, SemanticModel model, ImmutableArray<TypeToRemove> typesToRemove,
         Func<string, string> typeFolderGenerator, CancellationToken token)
      {
         var project = MoveTypeNodes(model, typesToRemove, typeFolderGenerator, document.Project, token);
         var newRoot = root.RemoveNodes(typesToRemove.Select(typeToRemove => typeToRemove.Declaration),
            SyntaxRemoveOptions.AddElasticMarker);
         var newSolution = project.Solution;
         newSolution = newSolution.WithDocumentSyntaxRoot(document.Id, newRoot);

         var newDocument = newSolution.GetProject(project.Id).GetDocument(document.Id);
         newRoot = await newDocument.GetSyntaxRootAsync(token).ConfigureAwait(false);
         var newModel = await newDocument.GetSemanticModelAsync(token).ConfigureAwait(false);
         var newUsings = newRoot.GenerateUsingDirectives(newModel);

         newRoot = newRoot.RemoveNodes(newRoot.DescendantNodes(node => true).OfType<UsingDirectiveSyntax>(),
            SyntaxRemoveOptions.AddElasticMarker);
         newRoot = (newRoot as CompilationUnitSyntax)?.WithUsings(newUsings);

         return newSolution.WithDocumentSyntaxRoot(document.Id, newRoot);
      }

      private static Project MoveTypeNodes(
         SemanticModel model, ImmutableArray<TypeToRemove> typesToRemove, Func<string, string> typeFolderGenerator,
         Project project, CancellationToken token)
      {
         var projectName = project.Name;
         foreach (var typeToRemove in typesToRemove)
         {
            token.ThrowIfCancellationRequested();

            var fileName = $"{typeToRemove.Symbol.Name}.cs";
            var containingNamespace = typeToRemove.Symbol.GetContainingNamespace();
            var typeFolder = typeFolderGenerator(containingNamespace).Replace(projectName, string.Empty);
            if (typeFolder.StartsWith("\\"))
            {
               typeFolder = typeFolder.Remove(0, 1);
            }

            project =
               project.AddDocument(fileName,
                  typeToRemove.Declaration.GetCompilationUnitForType(model, containingNamespace),
                  !string.IsNullOrWhiteSpace(typeFolder) ? new[] {typeFolder} : null).Project;
         }

         return project;
      }
   }
}