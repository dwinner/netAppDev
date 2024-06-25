using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RoadmapAnalyzer.Rules
{
   [DiagnosticAnalyzer(LanguageNames.CSharp)]
   public sealed class AddArticleToParameterAnalyzer : DiagnosticAnalyzer
   {
      public const string DiagnosticId = "Pretty0001";
      internal const string Category = "Naming";
      internal static readonly LocalizableString Title = "Add the indefinite article for the parameter name";
      internal static readonly LocalizableString MessageFormat = "Add the indefinite article for the parameter '{0}'";

      internal static readonly DiagnosticDescriptor Rule =
         new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

      public override void Initialize(AnalysisContext context)
         => context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.Parameter);

      private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
      {
         var node = context.Node as ParameterSyntax;
         if (node == null)
         {
            return;
         }

         var parameterName = node.Identifier.Text;
         var mustReport = MustReport(parameterName);
         if (mustReport)
         {
            var diagnostic = Diagnostic.Create(Rule, node.Identifier.GetLocation(), parameterName);
            context.ReportDiagnostic(diagnostic);
         }
      }

      private static bool MustReport(string parameterName)
      {
         var mustReport = false;
         if (parameterName.Length >= 3)
         {
            if (parameterName.StartsWith("an", StringComparison.CurrentCulture))
            {
               if (!char.IsUpper(parameterName[2]))
               {
                  mustReport = true;
               }
            }
            else if (parameterName.StartsWith("a", StringComparison.CurrentCulture))
            {
               if (!char.IsUpper(parameterName[1]))
               {
                  mustReport = true;
               }
            }
            else
            {
               mustReport = true;
            }
         }
         else if (parameterName.Length == 2)
         {
            mustReport = parameterName[0] != 'a' || !char.IsUpper(parameterName[1]);
         }
         else
         {
            mustReport = true;
         }

         return mustReport;
      }
   }
}