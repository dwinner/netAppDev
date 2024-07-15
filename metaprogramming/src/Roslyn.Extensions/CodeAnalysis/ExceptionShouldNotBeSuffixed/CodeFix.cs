using System.Collections.Immutable;
using System.Composition;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslyn.Extensions.CodeAnalysis.ExceptionShouldNotBeSuffixed;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(CodeFix))]
[Shared]
public class CodeFix : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(Analyzer.DiagnosticId);

    public override Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var diagnostic = context.Diagnostics[0];
        context.RegisterCodeFix(
            action: CodeAction.Create(
                "Remove Exception suffix",
                cnlToken => RemoveSuffix(context.Document, diagnostic, cnlToken),
                equivalenceKey: "t"),
            diagnostic);

        return Task.CompletedTask;
    }

    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    static async Task<Document> RemoveSuffix(Document document, Diagnostic diagnostic, CancellationToken c)
    {
        var root = await document.GetSyntaxRootAsync(c);
        if (root!.FindNode(diagnostic.Location.SourceSpan) is not ClassDeclarationSyntax node)
        {
            return document;
        }

        var newName = node.Identifier.Text.Replace("Exception", string.Empty);
        var newRoot = root.ReplaceNode(node, node.WithIdentifier(SyntaxFactory.Identifier(newName)));
        return document.WithSyntaxRoot(newRoot);
    }
}