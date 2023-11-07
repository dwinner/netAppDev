using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static GatheringOverrides.CodeGeneration;
using static Microsoft.CodeAnalysis.Accessibility;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace GatheringOverrides
{
   /// <summary>
   ///    Symbol extensions
   /// </summary>
   internal static class SymbolExtensions
   {
      private const int InitialCapacity = 0x80;
      private const string DefaultNoSummaryText = "No description";

      // TOREFACTOR: Had better use circular memory for this - to avoid memory entropy
      private static readonly Dictionary<string, ISet<ITypeSymbol>> _BaseTypeCache =
         new Dictionary<string, ISet<ITypeSymbol>>();

      private static readonly Func<ISymbol, bool> _IsAccessableToOverride = symbol =>
         (symbol.IsVirtual || symbol.IsOverride)
         && !symbol.IsSealed && !symbol.IsAbstract
         && (symbol.DeclaredAccessibility == Protected
             || symbol.DeclaredAccessibility == Public
             || symbol.DeclaredAccessibility == ProtectedOrInternal);

      /// <summary>
      ///    Get base types for <paramref name="typeSymbol" />
      /// </summary>
      /// <param name="typeSymbol">Type Symbol</param>
      /// <param name="hierarchyFilter">Type hierarchy filter</param>
      /// <returns>Base types</returns>
      internal static IEnumerable<ITypeSymbol> GetBaseTypes(this ITypeSymbol typeSymbol,
         TypeHierarchyFilter hierarchyFilter = TypeHierarchyFilter.All)
      {
         var typeName = typeSymbol.Name;
         if (_BaseTypeCache.ContainsKey(typeName))
         {
            return _BaseTypeCache[typeName];
         }

         var typeSymbols = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
         typeSymbol.GatherBaseTypes(typeSymbols);
         _BaseTypeCache[typeName] = typeSymbols;
         var filterBehavior = FilterBehavior(hierarchyFilter, typeName);
         typeSymbols.RemoveWhere(symbol => filterBehavior(symbol));

         return typeSymbols;

         Func<ITypeSymbol, bool> FilterBehavior(TypeHierarchyFilter filter, string typeSymbolName)
         {
            Func<ITypeSymbol, bool> func;
            switch (filter)
            {
               case TypeHierarchyFilter.All:
                  func = _ => false;
                  break;

               case TypeHierarchyFilter.ExcludeItselt:
                  func = symbol => symbol.Name.Equals(typeSymbolName, StringComparison.CurrentCulture);
                  break;

               case TypeHierarchyFilter.OnlyItself:
                  func = symbol => !symbol.Name.Equals(typeSymbolName, StringComparison.CurrentCulture);
                  break;

               default:
                  throw new ArgumentOutOfRangeException(nameof(hierarchyFilter), hierarchyFilter, null);
            }

            return func;
         }
      }

      /// <summary>
      ///    Recusrively visits all base types and saves them in <paramref name="typeSet" />
      /// </summary>
      /// <param name="typeSymbol">Type symbol - the lowest in inheritance chain</param>
      /// <param name="typeSet">Type symbols for being saved</param>
      private static void GatherBaseTypes(this ITypeSymbol typeSymbol, ISet<ITypeSymbol> typeSet)
      {
         while (true)
         {
            if (typeSymbol == null)
            {
               return;
            }

            typeSet.Add(typeSymbol);
            if (typeSymbol.Interfaces != null)
            {
               foreach (var @interface in typeSymbol.Interfaces)
               {
                  typeSet.Add(@interface);
                  @interface.GatherBaseTypes(typeSet);
               }
            }

            typeSymbol = typeSymbol.BaseType;
         }
      }

      internal static IEnumerable<ISymbol> GetOverridableSymbols(IEnumerable<ITypeSymbol> baseTypes) =>
         baseTypes
            .SelectMany(typeSymbol => typeSymbol.GetMembers())
            .Where(_IsAccessableToOverride)
            .ToList();

      private static string GetAccessModifiers(this ISymbol symbol)
      {
         string accessModifiers;
         var accessibility = symbol.DeclaredAccessibility;
         switch (accessibility)
         {
            case NotApplicable:
               accessModifiers = string.Empty;
               break;

            case Private:
               accessModifiers = "private";
               break;

            case ProtectedAndInternal:
               accessModifiers = "private protected";
               break;

            case Protected:
               accessModifiers = "protected";
               break;

            case Internal:
               accessModifiers = "internal";
               break;

            case ProtectedOrInternal:
               accessModifiers = "protected internal";
               break;

            case Public:
               accessModifiers = "public";
               break;

            default:
               throw new ArgumentOutOfRangeException(nameof(accessibility));
         }

         return accessModifiers;
      }

      // FIXME: Methods can be comparable only by signature, NOT by name!
      internal static IEnumerable<IMethodSymbol> GetOverridableMethods(IEnumerable<ISymbol> accesibleSymbols) =>
         new HashSet<IMethodSymbol>(
            accesibleSymbols
               .OfType<IMethodSymbol>()
               .Where(methodSymbol => methodSymbol.MethodKind == MethodKind.Ordinary),
            SymbolEqualityComparer.Default);

      internal static IEnumerable<IPropertySymbol> GetOverridableProperties(IEnumerable<ISymbol> accesibleSymbols) =>
         new HashSet<IPropertySymbol>(accesibleSymbols.OfType<IPropertySymbol>(), SymbolEqualityComparer.Default);

      internal static string ToSignature(this IPropertySymbol propertySymbol)
      {
         var propertyName = propertySymbol.Name;
         var propertyTypeName = propertySymbol.Type.SimplifyTypeName();
         var accessModifiers = propertySymbol.GetAccessModifiers();
         var propertySignature = new StringBuilder($"{accessModifiers} override ", InitialCapacity);
         propertySignature.Append($"{propertyTypeName} {propertyName} ");
         var propertySugar = PropertySugar();
         propertySignature.Append(propertySugar);

         return propertySignature.ToString();

         string PropertySugar()
         {
            var property = "{ ";
            if (propertySymbol.IsReadOnly)
            {
               var getModifiers = propertySymbol.GetMethod.GetAccessModifiers();
               if (!string.Equals(accessModifiers, getModifiers, StringComparison.Ordinal))
               {
                  property += $"{getModifiers} ";
               }

               property += "get; }";
            }
            else if (propertySymbol.IsWriteOnly)
            {
               property += "private get; ";
               var setModifiers = propertySymbol.SetMethod.GetAccessModifiers();
               if (!string.Equals(accessModifiers, setModifiers, StringComparison.Ordinal))
               {
                  property += $"{setModifiers} ";
               }

               property += "set; }";
            }
            else
            {
               var getModifiers = propertySymbol.GetMethod.GetAccessModifiers();
               if (!string.Equals(accessModifiers, getModifiers, StringComparison.Ordinal))
               {
                  property += $"{getModifiers} ";
               }

               property += "get; ";
               var setModifiers = propertySymbol.SetMethod.GetAccessModifiers();
               if (!string.Equals(accessModifiers, setModifiers, StringComparison.Ordinal))
               {
                  property += $"{setModifiers} ";
               }

               property += "set; }";
            }

            return property;
         }
      }

      internal static string ToSignature(this IMethodSymbol methodSymbol)
      {
         var methodName = methodSymbol.Name;
         const string separator = ", ";

         if (methodSymbol.IsGenericMethod)
         {
            var typeParams = methodSymbol.TypeParameters;
            var joinedString = Gather("<", ">", separator, typeParams, symbol => symbol.Name);
            methodName += joinedString;
         }

         var returnTypeModifiers = GetTypeModifiers(methodSymbol.RefKind);
         var returnType = methodSymbol.ReturnType.GetRepresentableReturnType();
         if (returnTypeModifiers.Length > 0)
         {
            returnType = $"{returnTypeModifiers} {returnType}";
         }

         var accessModifiers = methodSymbol.GetAccessModifiers();
         var signature = new StringBuilder($"{accessModifiers} override ", InitialCapacity);

         signature.Append($"{returnType} {methodName}");
         var parameters = methodSymbol.Parameters;
         var parameterList = Gather("(", ")", separator, parameters, symbol =>
         {
            var paramModifiers = GetTypeModifiers(symbol.RefKind);
            var parameterDisplayType = symbol.Type.GetRepresentableReturnType(symbol);
            if (!string.IsNullOrEmpty(paramModifiers))
            {
               parameterDisplayType = $"{paramModifiers} {parameterDisplayType}";
            }

            return $"{parameterDisplayType} {symbol.Name}";
         });
         signature.Append(parameterList);

         return signature.ToString();
      }

      private static string GetTypeModifiers(RefKind returnRefKind)
      {
         string typeModifiers;
         switch (returnRefKind)
         {
            case RefKind.None:
               typeModifiers = string.Empty;
               break;

            case RefKind.Ref:
               typeModifiers = "ref";
               break;

            case RefKind.Out:
               typeModifiers = "out";
               break;

            case RefKind.In:
               typeModifiers = string.Empty;
               break;

            default:
               throw new ArgumentOutOfRangeException(nameof(returnRefKind));
         }

         return typeModifiers;
      }

      private static string GetRepresentableReturnType(this ITypeSymbol @this,
         IParameterSymbol parameterSymbol = null)
      {
         if (@this.SpecialType != SpecialType.None)
         {
            return @this.ToDisplayString();
         }

         string returnTypeName;
         switch (@this.TypeKind)
         {
            case TypeKind.Unknown:
            case TypeKind.Struct:
            case TypeKind.TypeParameter:
            case TypeKind.Class:
            case TypeKind.Delegate:
            case TypeKind.Dynamic:
            case TypeKind.Enum:
            case TypeKind.Error:
            case TypeKind.Interface:
            case TypeKind.Module:
            case TypeKind.Submission:
               returnTypeName = @this.Name;
               break;

            case TypeKind.Array:
            {
               var arrayTypeSymbol = (IArrayTypeSymbol) @this;
               returnTypeName = arrayTypeSymbol.ToDisplay(parameterSymbol);
            }
               break;

            case TypeKind.Pointer:
            {
               var pointerTypeSymbol = (IPointerTypeSymbol) @this;
               returnTypeName = pointerTypeSymbol.ToDisplay();
            }
               break;

            default:
               throw new ArgumentOutOfRangeException();
         }

         return returnTypeName;
      }

      private static string ToDisplay(this IPointerTypeSymbol @this)
      {
         var pointedAtType = @this.PointedAtType;
         var pointedType = pointedAtType.SimplifyTypeName();
         var returnTypeName = $"{pointedType}*";
         return returnTypeName;
      }

      internal static string SimplifyTypeName(this ITypeSymbol @this) =>
         @this.SpecialType == SpecialType.None
            ? @this.Name
            : @this.ToDisplayString();

      private static string ToDisplay(this IArrayTypeSymbol @this,
         IParameterSymbol parameterSymbol)
      {
         string returnTypeName;
         var arrayElementType = @this.ElementType;
         var elementTypeDisplay = arrayElementType.SimplifyTypeName();
         if (parameterSymbol?.IsParams == true)
         {
            returnTypeName = $"params {elementTypeDisplay}[]";
         }
         else
         {
            var arrayRank = @this.Rank;
            var arrayIndexSignature = "[";
            if (arrayRank >= 2)
            {
               for (var i = 2; i <= arrayRank; i++)
               {
                  arrayIndexSignature += ",";
               }
            }

            arrayIndexSignature += "]";
            returnTypeName = $"{elementTypeDisplay}{arrayIndexSignature}";
         }

         return returnTypeName;
      }

      internal static string GetSummary(this ISymbol @this, string noSummaryText = DefaultNoSummaryText)
      {
         const string start = "<summary>";
         const string end = "</summary>";
         const string stripMisc = @"<(.|\n)*?>";

         var docCommentXml = @this.GetDocumentationCommentXml(CultureInfo.CurrentCulture);
         var startPos = docCommentXml.IndexOf(start, StringComparison.Ordinal);
         var endPos = docCommentXml.IndexOf(end, StringComparison.Ordinal);
         var offset = startPos + start.Length;
         var summaryText = startPos != -1 && endPos != -1 && endPos > offset
            ? docCommentXml.Substring(offset, endPos - offset)
            : noSummaryText;
         summaryText = Regex.Replace(summaryText, stripMisc, @this.Name);

         return summaryText.Trim();
      }

      private static string Gather<T>(string start, string end, string separator,
         IEnumerable<T> sequence,
         Func<T, string> stringProducer)
      {
         var aggregator =
            sequence.Aggregate(start, (current, element) => $"{current}{stringProducer(element)}{separator}");
         if (aggregator.Length > start.Length + separator.Length)
         {
            aggregator = aggregator.Substring(0, aggregator.Length - separator.Length);
         }

         aggregator += end;
         return aggregator;
      }

      internal static AccessorListSyntax GetAccessorList(this IPropertySymbol @this, string indentation)
      {
         var propertyName = @this.Name;
         AccessorListSyntax accessorList;
         var propertyAccessors = @this.DeclaredAccessibility;

         if (@this.IsReadOnly)
         {
            var getAccessModifiers = @this.GetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var getAccessorDecl = GetWithBaseCallAccessorDeclaration(getAccessModifiers, propertyName);
            accessorList = AccessorList(List(new[] {getAccessorDecl}));
         }
         else if (@this.IsWriteOnly)
         {
            var setAccessModifiers = @this.SetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var setAccessorDecl =
               GetWithBaseCallAccessorDeclaration(indentation, setAccessModifiers, propertyName);
            accessorList = AccessorList(List(new[] {setAccessorDecl}));
         }
         else
         {
            var getAccessModifiers = @this.GetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var getAccessorDecl = GetWithBaseCallAccessorDeclaration(getAccessModifiers, propertyName);
            var setAccessModifiers = @this.SetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var setAccessorDecl =
               GetWithBaseCallAccessorDeclaration(string.Empty, setAccessModifiers, propertyName);
            accessorList = AccessorList(List(new[] {getAccessorDecl, setAccessorDecl}));
         }

         var reducedIndentation = " ".Repeat(indentation.Length / 2);
         return accessorList
            .WithOpenBraceToken(
               SyntaxKind.OpenBraceToken.BuildToken(new[] {LineFeed, Whitespace(reducedIndentation)}, new[] {LineFeed}))
            .WithCloseBraceToken(
               SyntaxKind.CloseBraceToken.BuildToken(new[] {Whitespace(reducedIndentation)}, new[] {LineFeed}));
      }

      private static SyntaxTokenList GenerateAccessModifiers(this ISymbol @this,
         Accessibility propertyAccessibility, string indentation)
      {
         SyntaxTokenList accessModifiers;
         var leadingTrivia = Whitespace($"{indentation}");
         var trailingTrivia = Space;
         var emptyTokens =
            TokenList(SyntaxKind.None.BuildToken(new[] {leadingTrivia}, new[] {Whitespace(string.Empty)}));

         switch (@this.DeclaredAccessibility)
         {
            case NotApplicable:
               accessModifiers = emptyTokens;
               break;

            case Private
               when propertyAccessibility != Private:
            {
               accessModifiers = TokenList(
                  SyntaxKind.PrivateKeyword.BuildToken(new[] {leadingTrivia}, new[] {trailingTrivia}));
               break;
            }

            case ProtectedAndInternal
               when propertyAccessibility != ProtectedAndInternal:
            {
               accessModifiers = TokenList(
                  SyntaxKind.PrivateKeyword.BuildToken(new[] {leadingTrivia}, new[] {trailingTrivia}),
                  SyntaxKind.ProtectedKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {trailingTrivia}));
            }
               break;

            case Protected
               when propertyAccessibility != Protected:
            {
               accessModifiers =
                  TokenList(SyntaxKind.ProtectedKeyword.BuildToken(new[] {leadingTrivia}, new[] {trailingTrivia}));
            }
               break;

            case Internal
               when propertyAccessibility != Internal:
            {
               accessModifiers =
                  TokenList(SyntaxKind.InternalKeyword.BuildToken(new[] {leadingTrivia}, new[] {trailingTrivia}));
            }
               break;

            case ProtectedOrInternal
               when propertyAccessibility != ProtectedOrInternal:
            {
               accessModifiers = TokenList(
                  SyntaxKind.ProtectedKeyword.BuildToken(new[] {leadingTrivia}, new[] {trailingTrivia}),
                  SyntaxKind.InternalKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {trailingTrivia}));
            }
               break;

            case Public
               when propertyAccessibility != Public:
            {
               accessModifiers =
                  TokenList(SyntaxKind.PublicKeyword.BuildToken(new[] {leadingTrivia}, new[] {trailingTrivia}));
            }
               break;

            default:
               accessModifiers = emptyTokens;
               break;
         }

         return accessModifiers;
      }

      internal static SyntaxTokenList GetAccessModifiersWithOverride(this ISymbol symbol, string indentation)
      {
         var accessibility = symbol.DeclaredAccessibility;
         var tokens = new List<SyntaxToken>();
         var indentationTrivia = Whitespace(" ".Repeat(indentation.Length / 2));
         var leadingTrivia = LineFeed;
         var trailingTrivia = Space;

         switch (accessibility)
         {
            case NotApplicable:
               break;

            case Private:
               tokens.Add(SyntaxKind.PrivateKeyword.BuildToken(new[] {indentationTrivia}, new[] {trailingTrivia}));
               break;

            case ProtectedAndInternal:
               tokens.Add(SyntaxKind.PrivateKeyword.BuildToken(new[] {indentationTrivia}, new[] {trailingTrivia}));
               tokens.Add(SyntaxKind.ProtectedKeyword.BuildToken(
                  new[] {leadingTrivia, Whitespace(indentation)}, new[] {trailingTrivia}));
               break;

            case Protected:
               tokens.Add(SyntaxKind.ProtectedKeyword.BuildToken(new[] {indentationTrivia}, new[] {trailingTrivia}));
               break;

            case Internal:
               tokens.Add(SyntaxKind.InternalKeyword.BuildToken(new[] {indentationTrivia}, new[] {trailingTrivia}));
               break;

            case ProtectedOrInternal:
               tokens.AddRange(new[]
               {
                  SyntaxKind.ProtectedKeyword.BuildToken(new[] {indentationTrivia}, new[] {trailingTrivia}),
                  SyntaxKind.InternalKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {trailingTrivia})
               });
               break;

            case Public:
               tokens.Add(SyntaxKind.PublicKeyword.BuildToken(new[] {indentationTrivia}, new[] {trailingTrivia}));
               break;

            default:
               throw new ArgumentOutOfRangeException(nameof(accessibility));
         }

         tokens.Add(SyntaxKind.OverrideKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {trailingTrivia}));
         var tokenCount = tokens.Count;
         var tokensWithIndentation = new SyntaxToken[tokenCount + 1];
         tokensWithIndentation[0] = SyntaxKind.None.BuildToken(new[] {Whitespace(indentation)}, new[] {LineFeed});
         for (var i = 1; i < tokensWithIndentation.Length; i++)
         {
            tokensWithIndentation[i] = tokens[i - 1];
         }

         var accessTokens = TokenList(tokensWithIndentation);

         return accessTokens;
      }

      internal static SyntaxNodeOrToken GetQualifiedByTypeNode(this ITypeSymbol typeArg)
      {
         SyntaxNodeOrToken qualifiedByTypeNode;
         switch (typeArg.TypeKind)
         {
            case TypeKind.Unknown:
            case TypeKind.Class:
            case TypeKind.Delegate:
            case TypeKind.Dynamic:
            case TypeKind.Enum:
            case TypeKind.Error:
            case TypeKind.Interface:
            case TypeKind.Module:
            case TypeKind.Struct:
            case TypeKind.TypeParameter:
            case TypeKind.Submission:
               qualifiedByTypeNode = IdentifierName(typeArg.SimplifyTypeName());
               break;

            case TypeKind.Array:
               var arrayTypeSymbol = (IArrayTypeSymbol) typeArg;
               var arrayType = arrayTypeSymbol.ElementType.SimplifyTypeName();
               var rank = arrayTypeSymbol.Rank;

               var arrayTypeSyntax = ArrayType(IdentifierName(arrayType));
               arrayTypeSyntax = rank >= 2
                  ? arrayTypeSyntax.WithRankSpecifiers(
                     SingletonList(
                        ArrayRankSpecifier(
                           SeparatedList<ExpressionSyntax>(BuildSyntaxNodesExtensions.GetRankTokens(rank))
                        )
                     )
                  )
                  : arrayTypeSyntax.WithRankSpecifiers(
                     SingletonList(
                        ArrayRankSpecifier(
                           SingletonSeparatedList<ExpressionSyntax>(
                              OmittedArraySizeExpression()
                           )
                        )
                     )
                  );

               qualifiedByTypeNode = arrayTypeSyntax;
               break;

            case TypeKind.Pointer:
               throw new InvalidOperationException("Pointer type cannot be used as type parameter");

            default:
               throw new ArgumentOutOfRangeException(nameof(typeArg));
         }

         return qualifiedByTypeNode;
      }
   }
}