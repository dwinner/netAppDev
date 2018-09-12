using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommentRemover.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace CommentRemover
{
   public static class WorkspaceCommentRemover
   {
      public static async Task RemoveCommentsFromSolutionAsync(string aSolutionFile)
      {
         var workspace = MSBuildWorkspace.Create();
         var solution = await workspace.OpenSolutionAsync(aSolutionFile).ConfigureAwait(false);
         Solution[] newSolution = {solution};

         foreach (
            var documentId in
               solution.ProjectIds.SelectMany(projectId => newSolution[0].GetProject(projectId).DocumentIds))
         {
            newSolution[0] = await RemoveCommentsAsync(newSolution[0], documentId).ConfigureAwait(false);
         }

         workspace.TryApplyChanges(newSolution[0]);
      }

      public static async Task RemoveCommentsFromProjectAsync(string aProjectFile)
      {
         var workspace = MSBuildWorkspace.Create();
         var originalProject = await workspace.OpenProjectAsync(aProjectFile).ConfigureAwait(false);
         var solution = originalProject.Solution;
         var newSolution = solution;
         var project = newSolution.GetProject(originalProject.Id);

         foreach (var documentId in project.DocumentIds)
         {
            newSolution = await RemoveCommentsAsync(newSolution, documentId).ConfigureAwait(false);
         }

         workspace.TryApplyChanges(newSolution);
      }

      private static async Task<Solution> RemoveCommentsAsync(Solution anOldSolution, DocumentId aDocumentId)
      {
         Solution newSolution = null;
         var document = anOldSolution.GetDocument(aDocumentId);
         if (Path.GetExtension(document.FilePath)?.ToLower() == ".cs")
         {
            var root = await document.GetSyntaxRootAsync().ConfigureAwait(false);
            var newRoot = await root.RemoveCommentsAsync().ConfigureAwait(false);
            newSolution = root != newRoot
               ? anOldSolution.WithDocumentSyntaxRoot(aDocumentId, newRoot)
               : anOldSolution;
         }

         return newSolution;
      }
   }
}