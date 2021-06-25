using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using RoadmapAnalyzer.Properties;

namespace RoadmapAnalyzer.Rules
{
   [DiagnosticAnalyzer(LanguageNames.CSharp)]
   public sealed class WcfOneWayOperationAnalyzer : DiagnosticAnalyzer
   {
      internal const string DiagnosticId = "WCF0001";
      internal const string Category = "WcfHints";
      internal static readonly LocalizableString Title = Resources.WcfOneWayOperation_Title;
      internal static readonly LocalizableString MessageFormat = Resources.WcfOneWayOperation_Title;

      internal static readonly DiagnosticDescriptor Rule =
         new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

      public override void Initialize(AnalysisContext context)
         => context.RegisterSyntaxNodeAction(AnalyzeOneWayOperation, SyntaxKind.MethodDeclaration);

      private void AnalyzeOneWayOperation(SyntaxNodeAnalysisContext context)
      {
         var method = context.Node as MethodDeclarationSyntax;
         if (method == null)
         {
            return;
         }

         var attributes = method.AttributeLists;
         if (attributes.Count <= 0)
         {
            return;
         }

         var operationContractType = typeof (OperationContractAttribute);
         var operationContractAssembly = operationContractType.GetTypeInfo().Assembly.GetName().Name;
         var issueOperationAttr =
            (from attributeListSyntax in attributes
               from attributeSyntax in attributeListSyntax.Attributes
               let attributeType = context.SemanticModel.GetTypeInfo(attributeSyntax).Type
               where
                  attributeType != null
                  && attributeType.Name == operationContractType.Name
                  && attributeType.ContainingAssembly.Name == operationContractAssembly
               from argument in attributeSyntax.ArgumentList.Arguments
               where
                  argument.NameEquals.Name.Identifier.Text == nameof(OperationContractAttribute.IsOneWay)
                  && argument.Expression.IsKind(SyntaxKind.TrueLiteralExpression)
               select attributeSyntax).FirstOrDefault();

         if (issueOperationAttr != null)
         {
            var returnType = context.SemanticModel.GetTypeInfo(method.ReturnType).Type;
            if (returnType != null && returnType.SpecialType != SpecialType.System_Void)
            {
               context.ReportDiagnostic(Diagnostic.Create(Rule, issueOperationAttr.GetLocation()));
            }
         }
      }
   }
}