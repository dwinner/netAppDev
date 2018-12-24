using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadmapAnalyzer.Rules;
using RoadmapAnalyzer.Tests.Api;

namespace RoadmapAnalyzer.Tests
{
   [TestClass]
   public class MustInvokeBaseMethodAnalyzerTests
   {
      [TestMethod]
      public void VerifySupportedDiagnostics()
      {
         var analyzer = new MustInvokeBaseMethodAnalyzer();
         var diagnostics = analyzer.SupportedDiagnostics;
         Assert.AreEqual(1, diagnostics.Length);

         var diagnostic = diagnostics[0];
         Assert.AreEqual(diagnostic.Title.ToString(), MustInvokeBaseMethodAnalyzer.Title,
            nameof(DiagnosticDescriptor.Title));
         Assert.AreEqual(diagnostic.MessageFormat.ToString(), MustInvokeBaseMethodAnalyzer.MessageFormat,
            nameof(DiagnosticDescriptor.MessageFormat));
         Assert.AreEqual(diagnostic.Category, MustInvokeBaseMethodAnalyzer.Category,
            nameof(DiagnosticDescriptor.Category));
         Assert.AreEqual(diagnostic.DefaultSeverity, MustInvokeBaseMethodAnalyzer.DefaultSeverity,
            nameof(DiagnosticDescriptor.DefaultSeverity));
      }

      [TestMethod]
      [Case("Case2")]
      public Task AnalyzeWhenBaseClassHasVirtualMethodWithoutMustInvokeAndIsOverridenWithBaseClassCallTestAsync()
         =>
            TestHelpers.RunAnalysisAsync<MustInvokeBaseMethodAnalyzer>(
               $@"Targets\{nameof(MustInvokeBaseMethodAnalyzerTests)}\Case2.cs",
               Array.Empty<string>());

      [TestMethod]
      [Case("Case3")]
      public Task AnalyzeWhenBaseClassHasVirtualMethodWithoutMustInvokeAndIsNotOverridenWithBaseClassCall()
         => TestHelpers.RunAnalysisAsync<MustInvokeBaseMethodAnalyzer>(
            $@"Targets\{nameof(MustInvokeBaseMethodAnalyzerTests)}\Case3.cs",
            new[] {MustInvokeBaseMethodAnalyzer.DiagnosticId}, diagnostics =>
            {
               Assert.AreEqual(1, diagnostics.Count(), nameof(Enumerable.Count));
               var diagnostic = diagnostics[0];
               var span = diagnostic.Location.SourceSpan;
               Assert.AreEqual(276, span.Start, nameof(span.Start));
               Assert.AreEqual(282, span.End, nameof(span.End));
            });
   }
}