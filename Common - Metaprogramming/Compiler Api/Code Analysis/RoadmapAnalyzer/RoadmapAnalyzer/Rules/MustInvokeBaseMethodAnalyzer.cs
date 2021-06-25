using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RoadmapAnalyzer.Rules
{
   [DiagnosticAnalyzer(LanguageNames.CSharp)]
   public sealed class MustInvokeBaseMethodAnalyzer : DiagnosticAnalyzer
   {
      public const string DiagnosticId = "MUST0001";
      public const string Category = "Usage";
      public const DiagnosticSeverity DefaultSeverity = DiagnosticSeverity.Error;

      public static readonly LocalizableString Title =
         "Find overriden methods that do not call the base class's method";

      public static readonly LocalizableString MessageFormat =
         "Virtual methods with [MustInvoke] must be invoked in overrides";

      private static readonly DiagnosticDescriptor _Rule
         = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DefaultSeverity, true);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(_Rule);

      public override void Initialize(AnalysisContext context)
         => context.RegisterSyntaxNodeAction(AnalyzeMethodDeclaration, SyntaxKind.MethodDeclaration);

      private static void AnalyzeMethodDeclaration(SyntaxNodeAnalysisContext context)
      {

         var methodDecl = context.Node as MethodDeclarationSyntax;
         if (methodDecl == null)
         {
            return;
         }

         var model = context.SemanticModel;
         var methodSymbol = model.GetDeclaredSymbol(methodDecl);
         if (!methodSymbol.IsOverride)
         {
            return;
         }

         var overriddenMethod = methodSymbol.OverriddenMethod;
         var hasAttribute =
            overriddenMethod.GetAttributes().Any(attribute => attribute.AttributeClass.Name == "MustInvokeAttribute");
         if (!hasAttribute)
         {
            return;
         }

         var isOverridenInvoked =
            methodDecl.DescendantNodes(node => true)
               .OfType<InvocationExpressionSyntax>()
               .Select(invocation => model.GetSymbolInfo(invocation.Expression).Symbol)
               .OfType<IMethodSymbol>()
               .Contains(overriddenMethod);
         if (isOverridenInvoked)
         {
            return;
         }

         context.ReportDiagnostic(Diagnostic.Create(_Rule, methodDecl.Identifier.GetLocation()));
      }
   }
}