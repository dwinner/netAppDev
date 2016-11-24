using System.IO;
using System.Runtime.InteropServices;
using CommentRemover.Extensions;
using EnvDTE;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using Document = EnvDTE.Document;

namespace CommentRemoverPackage
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(PackageGuidString)]
   [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string)]
   [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string)]
   [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionHasMultipleProjects_string)]
   [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionHasSingleProject_string)]
   public sealed class CommentRemoverPackage :
      Package      
   {
      public const string PackageGuidString = "4706c3b1-3694-439b-83c1-7b9378e01fe0";

      private /*DocumentEventsClass*/DocumentEvents _documentEvents;
      private DTE _dte;
      private VisualStudioWorkspace _workspace;
      private Events _dteEvents;

      protected override void Initialize()
      {
         var model = (IComponentModel) GetService(typeof (SComponentModel));
         _workspace = model.GetService<VisualStudioWorkspace>();
         _dte = (DTE) GetService(typeof (DTE));
         _dteEvents = _dte.Events;
         _documentEvents = /*(DocumentEventsClass)*/ _dteEvents.DocumentEvents;
         _documentEvents.DocumentSaved += OnDocumentSaved;         

         base.Initialize();
      }

      protected override void Dispose(bool disposing)
      {
         _documentEvents.DocumentSaved -= OnDocumentSaved;
         base.Dispose(disposing);
      }

      private void OnDocumentSaved(Document dteDocument)
      {
         var documentIds = _workspace.CurrentSolution.GetDocumentIdsWithFilePath(dteDocument.FullName);
         if (documentIds == null || documentIds.Length != 1)
         {
            return;
         }

         var documentId = documentIds[0];
         var document = _workspace.CurrentSolution.GetDocument(documentId);

         if (Path.GetExtension(document.FilePath) != ".cs")
         {
            return;
         }

         SyntaxNode root;
         if (!document.TryGetSyntaxRoot(out root))
         {
            return;
         }

         var newRoot = root.RemoveComments();
         if (newRoot == root)
         {
            return;
         }

         var newSolution = document.Project.Solution.WithDocumentSyntaxRoot(document.Id, newRoot);
         _workspace.TryApplyChanges(newSolution);
         dteDocument.Save();
      }      
   }
}