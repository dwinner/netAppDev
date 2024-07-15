using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslyn.Extensions.GDPR;

#pragma warning disable SA1000

public class GdprSyntaxReceiver : ISyntaxReceiver
{
    readonly List<TypeDeclarationSyntax> _candidates = new();

    internal IEnumerable<TypeDeclarationSyntax> Candidates => _candidates;

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is not TypeDeclarationSyntax typeSyntax)
        {
            return;
        }

        _candidates.Add(typeSyntax);
    }
}