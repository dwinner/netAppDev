using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using RoadmapAnalyzer.Properties;

namespace RoadmapAnalyzer.Rules
{
   [DiagnosticAnalyzer(LanguageNames.CSharp)]
   public sealed class ConsiderDateTimeOffsetAnalyzer : DiagnosticAnalyzer
   {
      public const string DiagnosticId = "DTA001";

      private const string Category = "Naming";

      private static readonly LocalizableString _Title = Resources.ConsiderDateTimeOffsetAnalyzer_Title;

      private static readonly LocalizableString _MessageFormat = Resources.ConsiderDateTimeOffsetAnalyzer_MessageFormat;

      private static readonly LocalizableString _Description = Resources.ConsiderDateTimeOffsetAnalyzer_Description;

      private const string HelpLinkUrl = "https://github.com/AlessandroDelSole/RoslynSuccinctly/wiki/DTA001";

      private static readonly DiagnosticDescriptor _Rule =
         new DiagnosticDescriptor(DiagnosticId, _Title, _MessageFormat, Category, DiagnosticSeverity.Warning, true,
            _Description, HelpLinkUrl);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(_Rule);

      public override void Initialize(AnalysisContext context)
         =>
            context.RegisterCompilationStartAction(compilationContext =>
            {
               var winStoreTypeDetector =
                  compilationContext.Compilation.GetTypeByMetadataName("Windows.Storage.StorageFile");
               var oDataTypeDetector =
                  compilationContext.Compilation.GetTypeByMetadataName("Microsoft.OData.Core.ODataAction");
               if (winStoreTypeDetector != null || oDataTypeDetector != null)
               {
                  compilationContext.RegisterSyntaxNodeAction(AnalyzeDateTime, SyntaxKind.IdentifierName);
               }
            });

      private void AnalyzeDateTime(SyntaxNodeAnalysisContext nodeContext)
      {
         // Get syntax node to analyze
         var root = nodeContext.Node;

         // If it's not an IdentifierName syntax node, return
         var identifier = root as IdentifierNameSyntax;
         if (identifier == null)
         {
            return;
         }

         // Get the symbol info for the DateTime declaration
         var namedTypeSymbol = nodeContext.SemanticModel.GetSymbolInfo(identifier).Symbol as INamedTypeSymbol;
         if (namedTypeSymbol != null && namedTypeSymbol.MetadataName == nameof(DateTime))
         {
            var diagnostic = Diagnostic.Create(_Rule, identifier.GetLocation(), _MessageFormat);
            nodeContext.ReportDiagnostic(diagnostic);
         }
      }
   }
}