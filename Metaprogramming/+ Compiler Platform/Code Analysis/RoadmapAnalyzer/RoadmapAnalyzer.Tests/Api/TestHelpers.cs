using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadmapAnalyzer.Tests.Targets;

namespace RoadmapAnalyzer.Tests.Api
{
   internal static class TestHelpers
   {
      internal static async Task VerifyActionAsync(
         IEnumerable<CodeAction> actions, string title, Document document, SyntaxTree tree, string[] expectedNewTexts)
      {
         var action = actions.FirstOrDefault(a => a.Title == title);
         Assert.IsTrue(action != null);

         var operation =
            (await action.GetOperationsAsync(new CancellationToken(false)).ConfigureAwait(false)).ToArray()[0] as
               ApplyChangesOperation;
         Assert.IsTrue(operation != null);

         var newDoc = operation.ChangedSolution.GetDocument(document.Id);

         var newTree = await newDoc.GetSyntaxTreeAsync().ConfigureAwait(false);
         var changes = newTree.GetChanges(tree);

         Assert.AreEqual(expectedNewTexts.Length, changes.Count, nameof(changes.Count));
         Array.ForEach(expectedNewTexts, newText =>
         {
            Assert.IsTrue(changes.Any(change => change.NewText == newText),
               string.Join($"{Environment.NewLine}{Environment.NewLine}",
                  changes.Select(change => $"Change text: {change.NewText}")));
         });
      }

      internal static async Task RunAnalysisAsync<T>(
         string path, string[] diagnosticIds, Action<ImmutableArray<Diagnostic>> diagnosticInspector = null)
         where T : DiagnosticAnalyzer, new()
      {
         var code = File.ReadAllText(path);
         var diagnostics = await GetDiagnosticsAsync(code, new T()).ConfigureAwait(false);
         Assert.AreEqual(diagnosticIds.Length, diagnostics.Length, nameof(Enumerable.Count));
         Array.ForEach(diagnosticIds, dId => Assert.IsTrue(diagnostics.Any(diagnostic => diagnostic.Id == dId), dId));
         diagnosticInspector?.Invoke(diagnostics);
      }

      internal static async Task<ImmutableArray<Diagnostic>> GetDiagnosticsAsync(
         string code, DiagnosticAnalyzer analyzer)
      {
         var document = Create(code);
         var compilation =
            (await document.Project.GetCompilationAsync().ConfigureAwait(false)).WithAnalyzers(
               ImmutableArray.Create(analyzer));
         return await compilation.GetAnalyzerDiagnosticsAsync().ConfigureAwait(false);
      }

      internal static Document Create(string code)
      {
         const string projectName = "Test";
         var projectId = ProjectId.CreateNewId(projectName);

         var solution =
            new AdhocWorkspace().CurrentSolution
               .AddProject(projectId,
                  projectName, projectName, LanguageNames.CSharp)
               .WithProjectCompilationOptions(projectId,
                  new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
               .AddMetadataReference(projectId,
                  MetadataReference.CreateFromFile(typeof (object).Assembly.Location))
               .AddMetadataReference(projectId,
                  MetadataReference.CreateFromFile(typeof (Enumerable).Assembly.Location))
               .AddMetadataReference(projectId,
                  MetadataReference.CreateFromFile(typeof (CSharpCompilation).Assembly.Location))
               .AddMetadataReference(projectId,
                  MetadataReference.CreateFromFile(typeof (Compilation).Assembly.Location))
               .AddMetadataReference(projectId,
                  MetadataReference.CreateFromFile(typeof (MustInvokeAttribute).Assembly.Location));

         var documentId = DocumentId.CreateNewId(projectId);
         solution = solution.AddDocument(documentId, "Test.cs", SourceText.From(code));

         return solution.GetProject(projectId).Documents.First();
      }
   }
}