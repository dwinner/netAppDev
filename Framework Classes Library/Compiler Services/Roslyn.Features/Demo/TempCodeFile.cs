using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Threading;

namespace FirstDiagnostic
{
   internal class MyClass
   {
      public DiagnosticDescriptor Rule { get; set; }

      public void AnalyzeNode(SyntaxNode node, SemanticModel semanticModel, Action<Diagnostic> addDiagnostic,
          CancellationToken cancellationToken)
      {
         var localDeclaration = (LocalDeclarationStatementSyntax)node;

         // Only consider local variable declarations that aren't already const. 
         if (localDeclaration.Modifiers.Any(SyntaxKind.ConstKeyword))
         {
            return;
         }

         // Ensure that all variables in the local declaration have initializers that 
         // are assigned with constant values. 
         foreach (var variable in localDeclaration.Declaration.Variables)
         {
            var initializer = variable.Initializer;
            if (initializer == null)
            {
               return;
            }

            var constantValue = semanticModel.GetConstantValue(initializer.Value);
            if (!constantValue.HasValue)
            {
               return;
            }
         }

         // Perform data flow analysis on the local declaration. 
         var dataFlowAnalysis = semanticModel.AnalyzeDataFlow(localDeclaration);

         // Retrieve the local symbol for each variable in the local declaration 
         // and ensure that it is not written outside of the data flow analysis region. 
         foreach (var variable in localDeclaration.Declaration.Variables)
         {
            var variableSymbol = semanticModel.GetDeclaredSymbol(variable);
            if (dataFlowAnalysis.WrittenOutside.Contains(variableSymbol))
            {
               return;
            }
         }

         addDiagnostic(Diagnostic.Create(Rule, node.GetLocation()));
      }
   }
}