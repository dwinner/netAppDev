using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RoadmapAnalyzer.Properties;

namespace RoadmapAnalyzer.Rules
{
   [DiagnosticAnalyzer(LanguageNames.CSharp)]
   public sealed class AddAsyncSuffixAnalyzer : DiagnosticAnalyzer
   {
      public const string DiagnosticId = "SUC0001";
      private const string Category = "Naming";
      private static readonly LocalizableString _Title = Resources.AddAsyncSuffix_Title;
      private static readonly LocalizableString _MessageFormat = Resources.AddAsyncSuffix_MessageFormat;
      private static readonly LocalizableString _Description = Resources.AddAsyncSuffix_Description;

      private static readonly DiagnosticDescriptor _Rule = new DiagnosticDescriptor(DiagnosticId, _Title, _MessageFormat,
         Category, DiagnosticSeverity.Warning, true, _Description);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(_Rule);

      public override void Initialize(AnalysisContext context)
         => context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Method);

      private static void AnalyzeSymbol(SymbolAnalysisContext context)
      {
         var methodSymbol = (IMethodSymbol) context.Symbol;
         if (!methodSymbol.IsAsync)
         {
            return;
         }

         if (!methodSymbol.Name.ToLowerInvariant().EndsWith("async"))
         {
            var diagnostic = Diagnostic.Create(_Rule, methodSymbol.Locations[0], methodSymbol.Name);
            context.ReportDiagnostic(diagnostic);
         }
      }
   }
}