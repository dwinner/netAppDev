using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Roslyn.Extensions.CodeAnalysis.ExceptionShouldNotBeSuffixed;

#pragma warning disable RS2008
#pragma warning disable SA1000

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Analyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "PP0001";

    public static readonly DiagnosticDescriptor BrokenRule = new(
        DiagnosticId,
        "ExceptionShouldNotBeSuffixed",
        "The use of the word 'Exception' should not be added as a suffix - create a well understood and self explanatory name for the exception",
        "Naming",
        DiagnosticSeverity.Error,
        true,
        null,
        string.Empty,
        Array.Empty<string>());

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(BrokenRule);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.RegisterSyntaxNodeAction(
            HandleClassDeclaration,
            ImmutableArray.Create(
                SyntaxKind.ClassDeclaration));
    }

    void HandleClassDeclaration(SyntaxNodeAnalysisContext context)
    {
        var classDecl = context.Node as ClassDeclarationSyntax;
        if (classDecl?.BaseList?.Types == null)
        {
            return;
        }

        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDecl);
        if (classSymbol?.BaseType is null)
        {
            return;
        }

        var exceptionType = context.Compilation.GetTypeByMetadataName("System.Exception");
        if (SymbolEqualityComparer.Default.Equals(classSymbol.BaseType, exceptionType) &&
            classDecl.Identifier.Text.EndsWith("Exception", StringComparison.InvariantCulture))
        {
            var diagnostic = Diagnostic.Create(BrokenRule, classDecl.Identifier.GetLocation());
            context.ReportDiagnostic(diagnostic);
        }
    }
}