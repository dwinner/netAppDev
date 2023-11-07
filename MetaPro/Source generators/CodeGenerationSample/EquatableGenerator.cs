using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace CodeGenerationSample
{
   [Generator]
   public class EquatableGenerator : ISourceGenerator
   {
      private const string AttributeText = @"
using System;
namespace CodeGenerationSample
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class ImplementEquatableAttribute : Attribute
    {
        public ImplementEquatableAttribute() { }
    }
}
";

      public void Initialize(GeneratorInitializationContext context)
      {
//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif
//            Debug.WriteLine("Initialize Code Generator");
         // Register a syntax receiver that will be created for each generation pass
         context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
      }

      public void Execute(GeneratorExecutionContext context)
      {
         Debug.WriteLine("Execute code generator");
         // add the attribute text
         var eqAttrImpl = SourceText.From(AttributeText, Encoding.UTF8);
         context.AddSource("ImplementEquatableAttribute", eqAttrImpl);

         if (context.SyntaxReceiver is not SyntaxReceiver syntaxReceiver)
         {
            return;
         }

         var options = (context.Compilation as CSharpCompilation)?.SyntaxTrees[0].Options as CSharpParseOptions;
         Compilation compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(eqAttrImpl, options));
         var attributeSymbol = compilation.GetTypeByMetadataName("CodeGenerationSample.ImplementEquatableAttribute");

         List<ITypeSymbol> typeSymbols =
            (from @class in syntaxReceiver.CandidateClasses
               let model = compilation.GetSemanticModel(@class.SyntaxTree)
               select model.GetDeclaredSymbol(@class)
               into typeSymbol
               where typeSymbol!.GetAttributes().Any(attr =>
                  attr.AttributeClass!.Equals(attributeSymbol, SymbolEqualityComparer.Default))
               select typeSymbol).Cast<ITypeSymbol>().ToList();

         foreach (INamedTypeSymbol typeSymbol in typeSymbols)
         {
            string classSource = GetClassSource(typeSymbol);
            context.AddSource(typeSymbol.Name, SourceText.From(classSource, Encoding.UTF8));
         }
      }

      private static string GetClassSource(ISymbol typeSymbol)
      {
         string namespaceName = typeSymbol.ContainingNamespace.ToDisplayString();

         StringBuilder source = new($@"
using System;

namespace {namespaceName}
{{
    public partial class {typeSymbol.Name} : IEquatable<{typeSymbol.Name}>
    {{
        private static partial bool IsTheSame({typeSymbol.Name}? left, {typeSymbol.Name}? right);

        public override bool Equals(object? obj) => this == obj as {typeSymbol.Name};

        public bool Equals({typeSymbol.Name}? other) => this == other;

        public static bool operator==({typeSymbol.Name}? left, {typeSymbol.Name}? right) => 
            IsTheSame(left, right);

        public static bool operator!=({typeSymbol.Name}? left, {typeSymbol.Name}? right) =>
            !(left == right);

    }}
}}
");
         return source.ToString();
      }
   }

   /// <summary>
   ///    Created on demand before each generation pass
   /// </summary>
   internal class SyntaxReceiver : ISyntaxReceiver
   {
      public List<ClassDeclarationSyntax> CandidateClasses { get; } = new();

      /// <summary>
      ///    Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for
      ///    generation
      /// </summary>
      public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
      {
         // any class with at least one attribute is a candidate for the method generation
         if (syntaxNode is ClassDeclarationSyntax { AttributeLists: { Count: > 0 } } classDeclarationSyntax)
         {
            CandidateClasses.Add(classDeclarationSyntax);
         }
      }
   }
}