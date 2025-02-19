﻿using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SampleCodeAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class StringToUpperToLowerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "StringToUpperToLowerAnalyzer";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, 
        // you can use regular strings for Title and MessageFormat.
        // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Localizing%20Analyzers.md 
        // for more on localization
        private static readonly LocalizableString Title = 
            new LocalizableResourceString(nameof(Resources.ToUpperToLowerAnalyzerTitle), 
                                          Resources.ResourceManager, 
                                          typeof(Resources));
        private static readonly LocalizableString MessageFormat = 
            new LocalizableResourceString(nameof(Resources.ToUpperToLowerAnalyzerMessageFormat), 
                                          Resources.ResourceManager, 
                                          typeof(Resources));
        private static readonly LocalizableString Description = 
            new LocalizableResourceString(nameof(Resources.ToUpperToLowerAnalyzerDescription), 
                                          Resources.ResourceManager, 
                                          typeof(Resources));
        private const string Category = "Performance";

        private static DiagnosticDescriptor Rule = 
            new DiagnosticDescriptor(DiagnosticId, 
                                     Title, 
                                     MessageFormat, 
                                     Category, 
                                     DiagnosticSeverity.Warning, 
                                     isEnabledByDefault: true, 
                                     description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return ImmutableArray.Create(Rule);
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;
            var memberAccessExpression = invocationExpression.Expression as MemberAccessExpressionSyntax;
            var memberName = memberAccessExpression?.Name.ToString();
            if (memberName == "ToUpper" || memberName == "ToLower")
            {
                var diagnostic = Diagnostic.Create(Rule, memberAccessExpression.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
