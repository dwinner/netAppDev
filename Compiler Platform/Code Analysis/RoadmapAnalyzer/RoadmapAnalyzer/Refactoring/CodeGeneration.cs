using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Formatting;

namespace RoadmapAnalyzer.Refactoring
{
   internal static class CodeGeneration
   {
      private static readonly SyntaxAnnotation _UpdatedPropertyAnnotation = new SyntaxAnnotation("UpdatedProperty");

      internal static CompilationUnitSyntax ImplementFullProperty(CompilationUnitSyntax root, SemanticModel model,
         PropertyDeclarationSyntax propertyDecl, Workspace workspace)
      {
         var typeDecl = propertyDecl.FirstAncestorOrSelf<TypeDeclarationSyntax>();
         var propertyName = propertyDecl.Identifier.ValueText;
         var backingFieldName = $"_{char.ToLower(propertyName[0])}{propertyName.Substring(1)}";
         var propertyTypeSymbol = model.GetDeclaredSymbol(propertyDecl).Type;

         root = root.ReplaceNodes(new SyntaxNode[] { propertyDecl, typeDecl },
            (original, updated) => original.IsKind(SyntaxKind.PropertyDeclaration)
               ? ExpandProperty((PropertyDeclarationSyntax)original, (PropertyDeclarationSyntax)updated,
                  backingFieldName) as SyntaxNode
               : ExpandType((TypeDeclarationSyntax)updated, propertyTypeSymbol,
                  backingFieldName, workspace) as SyntaxNode);

         return root;
      }

      private static TypeDeclarationSyntax ExpandType(TypeDeclarationSyntax updated,
         ITypeSymbol typeSymbol, string backingFieldName, Workspace workspace)
         => updated.WithBackingField(typeSymbol, backingFieldName, workspace);

      private static TypeDeclarationSyntax WithBackingField(this TypeDeclarationSyntax node, ITypeSymbol typeSymbol,
         string backingFieldName, Workspace workspace)
      {
         var property =
            node.ChildNodes().FirstOrDefault(n => n.HasAnnotation(_UpdatedPropertyAnnotation)) as
               PropertyDeclarationSyntax;
         if (property == null)
         {
            return null;
         }

         var fieldDecl = GenerateBackingField(typeSymbol, backingFieldName, workspace);
         node = node.InsertNodesBefore(property, new[] { fieldDecl });
         return node;
      }

      private static MemberDeclarationSyntax GenerateBackingField(ITypeSymbol typeSymbol, string backingFieldName,
         Workspace workspace)
      {
         var generator = SyntaxGenerator.GetGenerator(workspace, LanguageNames.CSharp);
         var type = generator.TypeExpression(typeSymbol);
         var typeStr = type.ToString();
         var fieldDecl = ParseMember($"private {typeStr} {backingFieldName};") as FieldDeclarationSyntax;

         return fieldDecl;
      }

      private static MemberDeclarationSyntax ParseMember(string member)
      {
         var classDecl =
            SyntaxFactory.ParseCompilationUnit($"class x {{{Environment.NewLine}{member}{Environment.NewLine}}}")
               .Members[0] as ClassDeclarationSyntax;

         var decl = classDecl?.Members[0];
         return decl?.WithAdditionalAnnotations(Formatter.Annotation);
      }

      private static PropertyDeclarationSyntax ExpandProperty(BasePropertyDeclarationSyntax original,
         PropertyDeclarationSyntax updated, string backingFieldName)
      {
         var accessors = original.AccessorList.Accessors;
         var getter = accessors.FirstOrDefault(accessorDecl => accessorDecl.Kind() == SyntaxKind.GetAccessorDeclaration);
         var returnFieldStatement = SyntaxFactory.ParseStatement($"return {backingFieldName};");
         getter =
            getter.WithBody(SyntaxFactory.Block(SyntaxFactory.SingletonList(returnFieldStatement)))
               .WithSemicolonToken(default(SyntaxToken));

         var setter = accessors.FirstOrDefault(accessorDecl => accessorDecl.Kind() == SyntaxKind.SetAccessorDeclaration);
         var setPropertyStatement = SyntaxFactory.ParseStatement($"{backingFieldName} = value;");
         setter =
            setter.WithBody(SyntaxFactory.Block(SyntaxFactory.SingletonList(setPropertyStatement)))
               .WithSemicolonToken(default(SyntaxToken));

         updated =
            updated.WithAccessorList(SyntaxFactory.AccessorList(SyntaxFactory.List(new[] { getter, setter })))
               .WithAdditionalAnnotations(Formatter.Annotation)
               .WithAdditionalAnnotations(_UpdatedPropertyAnnotation);

         return updated;
      }
   }
}