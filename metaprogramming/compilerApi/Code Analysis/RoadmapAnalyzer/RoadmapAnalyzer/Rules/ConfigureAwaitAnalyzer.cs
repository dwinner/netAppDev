using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RoadmapAnalyzer.Rules
{
   [DiagnosticAnalyzer(LanguageNames.CSharp)]
   public sealed class ConfigureAwaitAnalyzer : DiagnosticAnalyzer
   {
      internal const string Id = "A101";

      private static readonly DiagnosticDescriptor _Desc =
         new DiagnosticDescriptor(Id,
            "Await expressions on Task or Task<T> should use ConfigureAwait to avoid deadlocks.",
            "Await expression lacks a ConfigureAwait and could cause deadlocks.",
            "Design",
            DiagnosticSeverity.Warning,
            true);

      static readonly ImmutableArray<SyntaxKind> _Kinds = ImmutableArray.Create(SyntaxKind.AwaitExpression);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(_Desc);

      public override void Initialize(AnalysisContext context)
      {
         context.RegisterSyntaxNodeAction(AnalyzeNode, _Kinds);
      }

      void AnalyzeNode(SyntaxNodeAnalysisContext context)
      {
         var model = context.SemanticModel;
         var compilation = model.Compilation;

         if (compilation.Options.OutputKind != OutputKind.DynamicallyLinkedLibrary)
         {
            return;
         }

         var awaitExpr = (AwaitExpressionSyntax) context.Node;
         var info = model.GetAwaitExpressionInfo(awaitExpr);
         if (info.IsDynamic)
         {
            return;
         }

         var operand = model.GetTypeInfo(awaitExpr.Expression);
         if (operand.Type is INamedTypeSymbol namedType)
         {
            var noAwaiter = namedType.IsGenericType
               ? Equals(namedType.ConstructedFrom, compilation.GetTypeByMetadataName(typeof (Task<>).FullName))
               : Equals(namedType, compilation.GetTypeByMetadataName(typeof (Task).FullName));
            if (noAwaiter)
            {
               context.ReportDiagnostic(Diagnostic.Create(_Desc, awaitExpr.GetLocation()));
            }
         }
      }
   }
}