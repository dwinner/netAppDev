using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RoadmapAnalyzer
{
   [DiagnosticAnalyzer(LanguageNames.CSharp)]
   public sealed class ConfigureAwaitAnalyzer : DiagnosticAnalyzer
   {
      public const string Id = "A101";

      private static readonly DiagnosticDescriptor Desc =
         new DiagnosticDescriptor(Id,
            "Await expressions on Task or Task<T> should use ConfigureAwait to avoid deadlocks.",
            "Await expression lacks a ConfigureAwait and could cause deadlocks.",
            "Design",
            DiagnosticSeverity.Warning,
            true);

      static readonly ImmutableArray<SyntaxKind> Kinds = ImmutableArray.Create(SyntaxKind.AwaitExpression);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Desc);

      public override void Initialize(AnalysisContext context)
      {
         context.RegisterSyntaxNodeAction(AnalyzeNode, Kinds);
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
         var namedType = operand.Type as INamedTypeSymbol;
         if (namedType != null)
         {
            var noAwaiter = namedType.IsGenericType
               ? Equals(namedType.ConstructedFrom, compilation.GetTypeByMetadataName(typeof (Task<>).FullName))
               : Equals(namedType, compilation.GetTypeByMetadataName(typeof (Task).FullName));
            if (noAwaiter)
            {
               context.ReportDiagnostic(Diagnostic.Create(Desc, awaitExpr.GetLocation()));
            }
         }
      }
   }
}