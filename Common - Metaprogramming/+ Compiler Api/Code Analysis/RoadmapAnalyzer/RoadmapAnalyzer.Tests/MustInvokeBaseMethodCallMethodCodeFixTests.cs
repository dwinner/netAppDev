using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadmapAnalyzer.Fixes;
using RoadmapAnalyzer.Rules;
using RoadmapAnalyzer.Tests.Api;

namespace RoadmapAnalyzer.Tests
{
   [TestClass]
   public sealed class MustInvokeBaseMethodCallMethodCodeFixTests
   {
      [TestMethod]
      public void VerifyGetFixableDiagnosticIds()
      {
         var fix = new MustInvokeBaseMethodCodeFix();
         var ids = fix.FixableDiagnosticIds;

         Assert.AreEqual(1, ids.Count(), nameof(Enumerable.Count));
         Assert.AreEqual(MustInvokeBaseMethodAnalyzer.DiagnosticId, ids[0]);
      }

      [TestMethod]
      [Case("Case0")]
      public async Task VerifyGetFixes()
      {
         var code = File.ReadAllText(
            $@"Targets\{nameof(MustInvokeBaseMethodCallMethodCodeFixTests)}\Case0.cs");
         var document = TestHelpers.Create(code);
         var tree = await document.GetSyntaxTreeAsync().ConfigureAwait(false);
         var diagnostics = await TestHelpers.GetDiagnosticsAsync(
            code, new MustInvokeBaseMethodAnalyzer()).ConfigureAwait(false);
         //var sourceSpan = diagnostics[0].Location.SourceSpan;

         var actions = new List<CodeAction>();
         var codeActionRegistration = new Action<CodeAction, ImmutableArray<Diagnostic>>(
            (a, _) => { actions.Add(a); });

         var fix = new MustInvokeBaseMethodCodeFix();
         var codeFixContext = new CodeFixContext(document, diagnostics[0],
            codeActionRegistration, new CancellationToken(false));
         await fix.RegisterCodeFixesAsync(codeFixContext).ConfigureAwait(false);

         Assert.AreEqual(1, actions.Count, nameof(actions.Count));

         await TestHelpers.VerifyActionAsync(actions,
            MustInvokeBaseMethodCodeFix.CodeFixDescription, document,
            tree, new[] { "\r\n        {\r\n            base.Method();\r\n        }\r\n    " }).ConfigureAwait(false);
      }
   }
}