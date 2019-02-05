using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using static Microsoft.CodeAnalysis.Accessibility;

namespace GatheringOverrides
{
   /// <summary>
   ///    Symbol extensions
   /// </summary>
   public static class SymbolExtensions
   {
      private static readonly Dictionary<string, ISet<ITypeSymbol>> BaseTypeCache =
         new Dictionary<string, ISet<ITypeSymbol>>();

      private static readonly Func<ISymbol, bool> IsAccessableToOverride = symbol =>
         (symbol.IsVirtual || symbol.IsOverride)
         && !symbol.IsSealed && !symbol.IsAbstract
         && (symbol.DeclaredAccessibility == Protected
             || symbol.DeclaredAccessibility == Public
             || symbol.DeclaredAccessibility == ProtectedOrInternal);

      /// <summary>
      ///    Get base types for <paramref name="typeSymbol" />
      /// </summary>
      /// <param name="typeSymbol">Type Symbol</param>
      /// <param name="includeItself">true, if you want to include <paramref name="typeSymbol" /> itself, false - no, by default</param>
      /// <returns>Base types</returns>
      public static IEnumerable<ITypeSymbol> GetBaseTypes(this ITypeSymbol typeSymbol, bool includeItself = false)
      {
         var typeName = typeSymbol.Name;
         if (BaseTypeCache.ContainsKey(typeName))
         {
            return BaseTypeCache[typeName];
         }

         var typeSymbols = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
         GatherTypeSymbols(typeSymbols, typeSymbol);

         BaseTypeCache[typeName] = typeSymbols;
         if (!includeItself)
         {
            typeSymbols.RemoveWhere(symbol => symbol.Name.Equals(typeName, StringComparison.Ordinal));
         }

         return typeSymbols;
      }

      /// <summary>
      ///    Recusrively visits all base types and saves them in <paramref name="typeSet" />
      /// </summary>
      /// <param name="typeSet">Type symbols</param>
      /// <param name="typeSymbol">Type symbol</param>
      private static void GatherTypeSymbols(ISet<ITypeSymbol> typeSet, ITypeSymbol typeSymbol)
      { // TODO: Had better use return value instead of typeSet parameter
         while (true)
         {
            if (typeSymbol == null)
            {
               return;
            }

            typeSet.Add(typeSymbol);
            if (typeSymbol.Interfaces != null)
            {
               foreach (var symbolInterface in typeSymbol.Interfaces)
                  typeSet.Add(symbolInterface);

               foreach (var currentInterface in typeSymbol.Interfaces)
                  GatherTypeSymbols(typeSet, currentInterface);
            }

            typeSymbol = typeSymbol.BaseType;
         }
      }

      public static IList<ISymbol> GetOverridableSymbols(IEnumerable<ITypeSymbol> baseTypes) =>
         baseTypes
            .SelectMany(typeSymbol => typeSymbol.GetMembers())
            .Where(IsAccessableToOverride)
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

      public static IEnumerable<IMethodSymbol> GetOverridableMethods(IEnumerable<ISymbol> accesibleSymbols)
      {
         // NOTE: Methods can be comparable only by signature, NOT by name!
         var methodsToOverride = new HashSet<IMethodSymbol>(SymbolEqualityComparer.Default);
         var methodSymbols = accesibleSymbols.OfType<IMethodSymbol>()
            .Where(methodSymbol => methodSymbol.MethodKind == MethodKind.Ordinary);
         foreach (var symbol in methodSymbols)
            methodsToOverride.Add(symbol);

         return methodsToOverride;
      }

      public static IEnumerable<IPropertySymbol> GetOverridableProperties(IEnumerable<ISymbol> accesibleSymbols)
      {
         var propertiesToOverride = new HashSet<IPropertySymbol>(SymbolEqualityComparer.Default);
         var propertySymbols = accesibleSymbols.OfType<IPropertySymbol>();
         foreach (var symbol in propertySymbols)
            propertiesToOverride.Add(symbol);

         return propertiesToOverride;
      }

      public static string ToSignature(this IPropertySymbol propertySymbol)
      {
         var propertyName = propertySymbol.Name;
         var propertyTypeName = propertySymbol.Type.ToDisplayString();
         var accessModifiers = GetAccessModifiers(propertySymbol);
         var signature = new StringBuilder($"{accessModifiers} override ", 0x40);
         signature.Append($"{propertyTypeName} {propertyName} ");
         if (propertySymbol.IsReadOnly)
         {
            signature.Append("{ get; }");
         }
         else if (propertySymbol.IsWriteOnly)
         {
            signature.Append("{ set; }");
         }
         else
         {
            signature.Append("{ get; set; }");
         }

         return signature.ToString();
      }

      public static string ToSignature(this IMethodSymbol methodSymbol)
      {
         var methodName = methodSymbol.Name;
         var returnTypeSymbol = methodSymbol.ReturnType;
         var returnType = GetReturnTypeToDisplay(returnTypeSymbol);
         var accessModifiers = GetAccessModifiers(methodSymbol);
         var signature = new StringBuilder($"{accessModifiers} override ", 0x80);
         signature.Append($"{returnType} {methodName}");
         var parameters = methodSymbol.Parameters;

         var parameterList = new StringBuilder("(");
         if (parameters != null && parameters.Length > 0)
         {
            for (var i = 0; i < parameters.Length; i++)
            {
               var parameterSymbol = parameters[i];
               var parameterName = parameterSymbol.Name;
               parameterList.Append($"{GetReturnTypeToDisplay(parameterSymbol.Type)} {parameterName}");
               if (i != parameters.Length - 1)
               {
                  parameterList.Append(", ");
               }
            }
         }

         parameterList.Append(")");
         signature.Append(parameterList);

         return signature.ToString();
      }

      private static string GetReturnTypeToDisplay(ITypeSymbol returnTypeSymbol) =>
         returnTypeSymbol.SpecialType == SpecialType.None
            ? returnTypeSymbol.Name
            : returnTypeSymbol.ToDisplayString();

      public static string GetSummary(this ISymbol symbol)
      {
         const string start = "<summary>";
         const string end = "</summary>";
         const string stripMisc = @"<(.|\n)*?>";

         var docCommentXml = symbol.GetDocumentationCommentXml(CultureInfo.CurrentCulture);
         var startPos = docCommentXml.IndexOf(start, StringComparison.Ordinal);
         var endPos = docCommentXml.IndexOf(end, StringComparison.Ordinal);
         var offset = startPos + start.Length;
         var summaryText = startPos != -1 && endPos != -1 && endPos > offset
            ? docCommentXml.Substring(offset, endPos - offset)
            : "No description";
         summaryText = Regex.Replace(summaryText, stripMisc, symbol.Name);

         return summaryText;
      }

      private sealed class SymbolEqualityComparer : IEqualityComparer<ISymbol>
      {
         public static IEqualityComparer<ISymbol> Default { get; } = new SymbolEqualityComparer();
         public bool Equals(ISymbol x, ISymbol y) => x.Name.Equals(y.Name, StringComparison.CurrentCulture);
         public int GetHashCode(ISymbol obj) => obj.Name.GetHashCode();
      }
   }
}