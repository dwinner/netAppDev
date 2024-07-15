using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslyn.Extensions.GDPR;

#pragma warning disable RS1035

[Generator]
public class GdprSourceGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not GdprSyntaxReceiver receiver)
        {
            return;
        }

        if (!context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.GDPRReport", out var filename))
        {
            return;
        }

        var writer = File.CreateText(filename);
        writer.AutoFlush = true;

        var piiAttr =
            context.Compilation.GetTypeByMetadataName(
                "Fundamentals.Compliance.GDPR.PersonalIdentifiableInformationAttribute");
        foreach (var candidate in receiver.Candidates)
        {
            var memberNamesAndReasons = new List<(string MemberName, string Reason)>();
            var semanticModel = context.Compilation.GetSemanticModel(candidate.SyntaxTree);
            var symbols = new List<ISymbol>();
            if (candidate is RecordDeclarationSyntax record)
            {
                symbols.AddRange(
                    record.ParameterList!.Parameters.Select(
                        parameter => semanticModel.GetDeclaredSymbol(parameter)).OfType<ISymbol>());
            }

            foreach (var member in candidate.Members)
            {
                if (member is not PropertyDeclarationSyntax property)
                {
                    continue;
                }

                var propertySymbol = semanticModel.GetDeclaredSymbol(property);
                if (propertySymbol is not null)
                {
                    symbols.Add(propertySymbol);
                }
            }

            foreach (var symbol in symbols)
            {
                var attributes = symbol.GetAttributes();
                var attribute = attributes.FirstOrDefault(attrData =>
                    SymbolEqualityComparer.Default.Equals(attrData.AttributeClass?.OriginalDefinition, piiAttr));
                if (attribute is not null)
                {
                    memberNamesAndReasons.Add((symbol.Name, attribute.ConstructorArguments[0].Value!.ToString()));
                }
            }

            if (memberNamesAndReasons.Count > 0)
            {
                var baseNsDeclSyntax = candidate.Parent as BaseNamespaceDeclarationSyntax;
                var @namespace = baseNsDeclSyntax!.Name.ToString();
                writer.WriteLine($"Type: {@namespace}.{candidate.Identifier.ValueText}");
                writer.WriteLine("Members:");
                foreach (var (memberName, reason) in memberNamesAndReasons)
                {
                    var reasonText = string.IsNullOrEmpty(reason) ? "No reason provided" : reason;
                    writer.WriteLine($"  {memberName}: {reasonText}");
                }

                writer.WriteLine(string.Empty);
            }
        }
    }

    public void Initialize(GeneratorInitializationContext context) =>
        context.RegisterForSyntaxNotifications(() => new GdprSyntaxReceiver());
}