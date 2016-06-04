using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RoadmapAnalyzer
{
   [DiagnosticAnalyzer(LanguageNames.CSharp)]
   public class FieldNameAnalyzer : DiagnosticAnalyzer
   {
      public const string Id = "S101";

      private static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(Id,
         "Field names shouldn't start with an underscore.",
         "The name of field {0} starts with an underscore.", "Naming",
         DiagnosticSeverity.Warning, true);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Descriptor);

      public override void Initialize(AnalysisContext context)
      {
         context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Field);
      }

      private void AnalyzeSymbol(SymbolAnalysisContext context)
      {
         if (context.Symbol.Name.StartsWith("_") && context.Symbol.Name.Length > 1)
         {
            context.ReportDiagnostic(Diagnostic.Create(Descriptor, context.Symbol.Locations[0], context.Symbol.Name));
         }
      }
   }
}