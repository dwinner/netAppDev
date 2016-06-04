using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RoadmapAnalyzer.Rules
{
   [DiagnosticAnalyzer(LanguageNames.CSharp)]
   public class SingleStatementBodyAnalyzer : DiagnosticAnalyzer
   {
      private const string DiagnosticId = "S102";
      private const string Category = "Naming";
      private static readonly LocalizableString Title = "Single statement bodies should be surrounded by culry braces.";

      private static readonly LocalizableString MessageFormat =
         "'{0}' should use curly braces around the statement body.";

      private static readonly IDictionary<SyntaxKind, string> KindNames = new Dictionary<SyntaxKind, string>
      {
         {SyntaxKind.IfStatement, "if statement"},
         {SyntaxKind.ElseClause, "else clause"},
         {SyntaxKind.WhileStatement, "while statement"},
         {SyntaxKind.ForStatement, "for statement"},
         {SyntaxKind.ForEachStatement, "foreach statement"}
      };

      private static readonly DiagnosticDescriptor Rule =
         new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);

      public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

      public override void Initialize(AnalysisContext context)
      {
         context.RegisterSyntaxNodeAction(AnalyzeNode,
            SyntaxKind.IfStatement,
            SyntaxKind.ElseClause,
            SyntaxKind.WhileStatement,
            SyntaxKind.ForStatement,
            SyntaxKind.ForEachStatement);
      }

      private void AnalyzeNode(SyntaxNodeAnalysisContext context)
      {
         var body = default(StatementSyntax);
         var token = default(SyntaxToken);

         switch (context.Node.Kind())
         {
            case SyntaxKind.IfStatement:
               var ifStmt = (IfStatementSyntax) context.Node;
               body = ifStmt.Statement;
               token = ifStmt.IfKeyword;
               break;

            case SyntaxKind.ElseClause:
               var elseStmt = (ElseClauseSyntax) context.Node;
               body = elseStmt.Statement;
               token = elseStmt.ElseKeyword;
               break;

            case SyntaxKind.WhileStatement:
               var whileStmt = (WhileStatementSyntax) context.Node;
               body = whileStmt.Statement;
               token = whileStmt.WhileKeyword;
               break;

            case SyntaxKind.ForStatement:
               var forStmt = (ForStatementSyntax) context.Node;
               body = forStmt.Statement;
               token = forStmt.ForKeyword;
               break;

            case SyntaxKind.ForEachStatement:
               var forEachStmt = (ForEachStatementSyntax) context.Node;
               body = forEachStmt.Statement;
               token = forEachStmt.ForEachKeyword;
               break;
         }

         if (!body.IsKind(SyntaxKind.Block))
         {
            // else if { ... } is fine
            if (context.Node.IsKind(SyntaxKind.ElseClause) && body.IsKind(SyntaxKind.IfStatement))
            {
               return;
            }

            var location = token.GetLocation();
            var name = KindNames[context.Node.Kind()];
            context.ReportDiagnostic(Diagnostic.Create(Rule, location, name));
         }
      }
   }
}