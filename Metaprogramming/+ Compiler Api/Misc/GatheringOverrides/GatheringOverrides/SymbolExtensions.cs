using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static GatheringOverrides.TokenGeneration;
using static Microsoft.CodeAnalysis.Accessibility;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace GatheringOverrides
{
   /// <summary>
   ///    Symbol extensions
   /// </summary>
   public static class SymbolExtensions
   {
      private const int InitialCapacity = 0x80;
      private const string DefaultNoSummaryText = "No description";

      // TOREFACTOR: Had better use circular memory for this - to avoid entropy
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
      public static IEnumerable<ITypeSymbol> GetBaseTypes(this ITypeSymbol typeSymbol,
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
               // TOREFACTOR: Had better gather them in parallel
               foreach (var @interface in typeSymbol.Interfaces)
               {
                  typeSet.Add(@interface);
               }

               // TOREFACTOR: Why we need one more iteration here?!
               foreach (var currentInterface in typeSymbol.Interfaces)
               {
                  currentInterface.GatherBaseTypes(typeSet);
               }
            }

            typeSymbol = typeSymbol.BaseType;
         }
      }

      public static IList<ISymbol> GetOverridableSymbols(IEnumerable<ITypeSymbol> baseTypes) =>
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
               accessModifiers = String.Empty;
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
      public static IEnumerable<IMethodSymbol> GetOverridableMethods(IEnumerable<ISymbol> accesibleSymbols) =>
         new HashSet<IMethodSymbol>(
            accesibleSymbols
               .OfType<IMethodSymbol>()
               .Where(methodSymbol => methodSymbol.MethodKind == MethodKind.Ordinary),
            SymbolEqualityComparer.Default);

      public static IEnumerable<IPropertySymbol> GetOverridableProperties(IEnumerable<ISymbol> accesibleSymbols) =>
         new HashSet<IPropertySymbol>(accesibleSymbols.OfType<IPropertySymbol>(), SymbolEqualityComparer.Default);

      public static string ToSignature(this IPropertySymbol propertySymbol)
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
               if (!String.Equals(accessModifiers, getModifiers, StringComparison.Ordinal))
               {
                  property += $"{getModifiers} ";
               }

               property += "get; }";
            }
            else if (propertySymbol.IsWriteOnly)
            {
               property += "private get; ";
               var setModifiers = propertySymbol.SetMethod.GetAccessModifiers();
               if (!String.Equals(accessModifiers, setModifiers, StringComparison.Ordinal))
               {
                  property += $"{setModifiers} ";
               }

               property += "set; }";
            }
            else
            {
               var getModifiers = propertySymbol.GetMethod.GetAccessModifiers();
               if (!String.Equals(accessModifiers, getModifiers, StringComparison.Ordinal))
               {
                  property += $"{getModifiers} ";
               }

               property += "get; ";
               var setModifiers = propertySymbol.SetMethod.GetAccessModifiers();
               if (!String.Equals(accessModifiers, setModifiers, StringComparison.Ordinal))
               {
                  property += $"{setModifiers} ";
               }

               property += "set; }";
            }

            return property;
         }
      }

      public static string ToSignature(this IMethodSymbol methodSymbol)
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
            if (!String.IsNullOrEmpty(paramModifiers))
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
               typeModifiers = String.Empty;
               break;

            case RefKind.Ref:
               typeModifiers = "ref";
               break;

            case RefKind.Out:
               typeModifiers = "out";
               break;

            case RefKind.In:
               typeModifiers = String.Empty;
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

      public static string SimplifyTypeName(this ITypeSymbol @this) =>
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

      public static string GetSummary(this ISymbol @this, string noSummaryText = DefaultNoSummaryText)
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

      public static SyntaxTokenList GetModifierTokens(this ISymbol @this, string indentation)
      {
         var tokens = @this.DeclaredAccessibility.GetModifierTokens(indentation);
         tokens.Add(GetOverrideToken());
         return TokenList(tokens);
      }

      public static AccessorListSyntax GetAccessorList(this IPropertySymbol propertySymbol, string indentation)
      {
         var propertyName = propertySymbol.Name;
         AccessorListSyntax accessorList;
         var propertyAccessors = propertySymbol.DeclaredAccessibility;

         if (propertySymbol.IsReadOnly)
         {
            var getAccessModifiers = propertySymbol.GetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var getAccessorDecl = CodeGeneration.GetWithBaseCallAccessorDeclaration(getAccessModifiers, propertyName);
            accessorList = AccessorList(List<AccessorDeclarationSyntax>(new[] {getAccessorDecl}));
         }
         else if (propertySymbol.IsWriteOnly)
         {
            var setAccessModifiers = propertySymbol.SetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var setAccessorDecl = CodeGeneration.GetWithBaseCallAccessorDeclaration(indentation, setAccessModifiers, propertyName);
            accessorList = AccessorList(List<AccessorDeclarationSyntax>(new[] {setAccessorDecl}));
         }
         else
         {
            var getAccessModifiers = propertySymbol.GetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var getAccessorDecl = CodeGeneration.GetWithBaseCallAccessorDeclaration(getAccessModifiers, propertyName);
            var setAccessModifiers = propertySymbol.SetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var setAccessorDecl = CodeGeneration.GetWithBaseCallAccessorDeclaration(String.Empty, setAccessModifiers, propertyName);
            accessorList = AccessorList(List<AccessorDeclarationSyntax>(new[] {getAccessorDecl, setAccessorDecl}));
         }

         var reducedIndentation = " ".Repeat(indentation.Length / 2);
         return accessorList
            .WithOpenBraceToken(GetOpenBraceToken(reducedIndentation))
            .WithCloseBraceToken(GetCloseBraceToken(reducedIndentation));
      }

      private static SyntaxTokenList GenerateAccessModifiers(this ISymbol @this,
         Accessibility propertyAccessibility, string indentation)
      {
         SyntaxTokenList accessModifiers;
         var leadingTrivia = Whitespace($"{indentation}");
         var trailingTrivia = Space;
         var emptyTokens = TokenList(TokenGeneration.GetNoneToken(leadingTrivia));

         switch (@this.DeclaredAccessibility)
         {
            case NotApplicable:
               accessModifiers = emptyTokens;
               break;

            case Private
               when propertyAccessibility != Private:
            {
               accessModifiers = TokenList(GetPrivateToken(leadingTrivia, trailingTrivia));
               break;
            }

            case ProtectedAndInternal
               when propertyAccessibility != ProtectedAndInternal:
            {
               accessModifiers = TokenList(GetPrivateToken(leadingTrivia, trailingTrivia),
                  GetProtectedToken(trailingTrivia));
            }
               break;

            case Protected
               when propertyAccessibility != Protected:
            {
               accessModifiers = TokenList(GetProtectedTokens(leadingTrivia, trailingTrivia)
               );
            }
               break;

            case Internal
               when propertyAccessibility != Internal:
            {
               accessModifiers = TokenList(GetInternalToken(leadingTrivia, trailingTrivia));
            }
               break;

            case ProtectedOrInternal
               when propertyAccessibility != ProtectedOrInternal:
            {
               accessModifiers = TokenList(
                  GetProtectedToken(leadingTrivia, trailingTrivia),
                  GetInternalToken(trailingTrivia));
            }
               break;

            case Public
               when propertyAccessibility != Public:
            {
               accessModifiers = TokenList(GetPublicToken(leadingTrivia, trailingTrivia));
            }
               break;

            default:
               accessModifiers = emptyTokens;
               break;
         }

         return accessModifiers;
      }

      public static SyntaxTokenList GetAccessModifiersWithOverride(this ISymbol symbol, string indentation)
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
               tokens.Add(GetPrivateToken(indentationTrivia, trailingTrivia));
               break;

            case ProtectedAndInternal:
               tokens.Add(GetPrivateToken(indentationTrivia, trailingTrivia));
               tokens.Add(GetProtectedToken(indentation, leadingTrivia, trailingTrivia)
               );
               break;

            case Protected:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            case Internal:
               tokens.Add(GetInternalToken(indentationTrivia, trailingTrivia));
               break;

            case ProtectedOrInternal:
               tokens.AddRange(new []
               {
                  GetProtectedTokens(indentationTrivia, trailingTrivia),
                  GetInternalToken(trailingTrivia)
               });
               break;

            case Public:
               tokens.Add(GetPublicToken(indentationTrivia, trailingTrivia));
               break;

            default:
               throw new ArgumentOutOfRangeException(nameof(accessibility));
         }

         tokens.Add(GetOverrideToken(trailingTrivia));

         var tokenCount = tokens.Count;
         var tokensWithIndentation = new SyntaxToken[tokenCount + 1];
         tokensWithIndentation[0] = GetNoneToken(indentation);
         for (var i = 1; i < tokensWithIndentation.Length; i++)
         {
            tokensWithIndentation[i] = tokens[i - 1];
         }

         var accessTokens = TokenList(tokensWithIndentation);

         return accessTokens;
      }

      private static SyntaxToken GetInternalToken(SyntaxTrivia indentationTrivia, SyntaxTrivia trailingTrivia) =>
         Token(
            TriviaList(indentationTrivia),
            SyntaxKind.InternalKeyword,
            TriviaList(trailingTrivia)
         );

      private static SyntaxToken GetNoneToken(string indentation) =>
         Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.None,
            TriviaList(LineFeed));

      private static SyntaxToken GetProtectedTokens(SyntaxTrivia indentationTrivia, SyntaxTrivia trailingTrivia) =>
         Token(
            TriviaList(indentationTrivia),
            SyntaxKind.ProtectedKeyword,
            TriviaList(trailingTrivia)
         );

      private static SyntaxToken GetInternalToken(SyntaxTrivia trailingTrivia) =>
         Token(
            TriviaList(),
            SyntaxKind.InternalKeyword,
            TriviaList(trailingTrivia)
         );
   }
}