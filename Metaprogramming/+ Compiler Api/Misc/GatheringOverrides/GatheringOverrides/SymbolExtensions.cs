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
      /// <param name="onlySelf">truem if you need only type <paramref name="typeSymbol" /> itself</param>
      /// <returns>Base types</returns>
      public static IEnumerable<ITypeSymbol> GetBaseTypes(this ITypeSymbol typeSymbol,
      	 bool includeItself = false,
         bool onlySelf = false)
      {
         var typeName = typeSymbol.Name;
         if (BaseTypeCache.ContainsKey(typeName))
         {
            return BaseTypeCache[typeName];
         }

         var typeSymbols = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
         typeSymbol.GatherBaseTypes(typeSymbols);
         BaseTypeCache[typeName] = typeSymbols;

         // TODO: USE enums for choices between what function should be used for filtering
         if (!includeItself)
         {
            var filterStrategy = onlySelf
               ? (Func<ITypeSymbol, bool>) (symbol => !symbol.Name.Equals(typeName, StringComparison.Ordinal))
               : symbol => symbol.Name.Equals(typeName, StringComparison.Ordinal);
            typeSymbols.RemoveWhere(symbol => filterStrategy(symbol));
         }

         return typeSymbols;
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
               foreach (var symbolInterface in typeSymbol.Interfaces)
                  typeSet.Add(symbolInterface);

               foreach (var currentInterface in typeSymbol.Interfaces)
                  currentInterface.GatherBaseTypes(typeSet);
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

      // FIXME: Methods can be comparable only by signature, NOT by name!
      public static IEnumerable<IMethodSymbol> GetOverridableMethods(IEnumerable<ISymbol> accesibleSymbols) =>
         new HashSet<IMethodSymbol>(
            accesibleSymbols.OfType<IMethodSymbol>()
               .Where(methodSymbol => methodSymbol.MethodKind == MethodKind.Ordinary), SymbolEqualityComparer.Default);

      public static IEnumerable<IPropertySymbol> GetOverridableProperties(IEnumerable<ISymbol> accesibleSymbols) =>
         new HashSet<IPropertySymbol>(accesibleSymbols.OfType<IPropertySymbol>(), SymbolEqualityComparer.Default);

      public static string ToSignature(this IPropertySymbol propertySymbol)
      {
         const string setOnlySugar = "{ set; }";
         const string setAndGetSugar = "{ get; set; }";
         const string getOnlySugar = "{ get; }";

         var propertyName = propertySymbol.Name;
         var propertyTypeName = propertySymbol.Type.ToDisplayString();
         var accessModifiers = GetAccessModifiers(propertySymbol);
         var signature = new StringBuilder($"{accessModifiers} override ", 0x40);	// TODO: Use shared threshold as const
         signature.Append($"{propertyTypeName} {propertyName} ");
         var propertySugar = !propertySymbol.IsReadOnly
            ? propertySymbol.IsWriteOnly
               ? setOnlySugar
               : setAndGetSugar
            : getOnlySugar;
         signature.Append(propertySugar);

         return signature.ToString();
      }

      public static string ToSignature(this IMethodSymbol methodSymbol)
      {
         var methodName = methodSymbol.Name;

         if (methodSymbol.IsGenericMethod)
         {
            var typeParameterSymbols = methodSymbol.TypeParameters.ToArray();

            // TODO: Use string.Join here
            var genericParameters = "<";
            for (var i = 0; i < typeParameterSymbols.Length; i++)
            {
               var genericParameterSymbol = typeParameterSymbols[i];
               genericParameters += genericParameterSymbol.Name;
               if (i != typeParameterSymbols.Length - 1)
               {
                  genericParameters += ", ";
               }
            }

            genericParameters += ">";
            methodName += genericParameters;
         }

         var returnTypeModifiers = GetTypeModifiers(methodSymbol.RefKind);
         var returnType = methodSymbol.ReturnType.GetReturnTypeToDisplay();
         if (returnTypeModifiers.Length > 0)
         {
            returnType = $"{returnTypeModifiers} {returnType}";
         }         

         var accessModifiers = methodSymbol.GetAccessModifiers();
         var signature = new StringBuilder($"{accessModifiers} override ", 0x80);

         signature.Append($"{returnType} {methodName}");
         var parameters = methodSymbol.Parameters;
         var parameterList = new StringBuilder("(");
         if (parameters != null && parameters.Length > 0)
         {
         	// TODO: Use string.Join with Enumerable.Aggregate
            for (var i = 0; i < parameters.Length; i++)
            {
               var parameterSymbol = parameters[i];
               var parameterRefKind = parameterSymbol.RefKind;
               var parameterReturnTypeModifiers = GetTypeModifiers(parameterRefKind);
               var parameterName = parameterSymbol.Name;
               var parameterType = parameterSymbol.Type;
               var parameterDisplayType = parameterType.GetReturnTypeToDisplay(parameterSymbol);
               if (!string.IsNullOrEmpty(parameterReturnTypeModifiers))
               {
                  parameterDisplayType = $"{parameterReturnTypeModifiers} {parameterDisplayType}";
               }

               parameterList.Append($"{parameterDisplayType} {parameterName}");
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

      private static string GetReturnTypeToDisplay(this ITypeSymbol @this,
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
               returnTypeName = ProcessArrayTypeKind(parameterSymbol, arrayTypeSymbol);
            }
               break;

            case TypeKind.Pointer:
            {
               var pointerTypeSymbol = (IPointerTypeSymbol) @this;
               returnTypeName = ProcessPointerTypeKind(pointerTypeSymbol);
            }
               break;

            default:
               throw new ArgumentOutOfRangeException();
         }

         return returnTypeName;

      }

      private static string ProcessPointerTypeKind(IPointerTypeSymbol pointerTypeSymbol)
      {
         var pointedAtType = pointerTypeSymbol.PointedAtType;
         var pointedType = pointedAtType.NormalizeTypeSymbol();
         var returnTypeName = $"{pointedType}*";
         return returnTypeName;
      }

      private static string NormalizeTypeSymbol(this ITypeSymbol pointedAtType) =>
         pointedAtType.SpecialType == SpecialType.None
            ? pointedAtType.Name
            : pointedAtType.ToDisplayString();

      private static string ProcessArrayTypeKind(IParameterSymbol parameterSymbol, IArrayTypeSymbol arrayTypeSymbol)
      {
         string returnTypeName;
         var arrayElementType = arrayTypeSymbol.ElementType;
         var elementTypeDisplay = arrayElementType.NormalizeTypeSymbol();
         if (parameterSymbol?.IsParams == true)	// FIXME: strange case with parameter usage here
         {
            returnTypeName = $"params {elementTypeDisplay}[]";
         }
         else
         {
            var arrayRank = arrayTypeSymbol.Rank;

            // TODO: Use string.Join
            var arrayIndexSignature = "[";
            if (arrayRank >= 2)
            {
               for (var i = 2; i <= arrayRank; i++)
                  arrayIndexSignature += ",";
            }

            arrayIndexSignature += "]";
            returnTypeName = $"{elementTypeDisplay}{arrayIndexSignature}";
         }

         return returnTypeName;
      }

      public static string GetSummary(this ISymbol symbol)
      {
         const string start = "<summary>";
         const string end = "</summary>";
         const string stripMisc = @"<(.|\n)*?>";
         const string noSummary = "No description";

         var docCommentXml = symbol.GetDocumentationCommentXml(CultureInfo.CurrentCulture);
         var startPos = docCommentXml.IndexOf(start, StringComparison.Ordinal);
         var endPos = docCommentXml.IndexOf(end, StringComparison.Ordinal);
         var offset = startPos + start.Length;
         var summaryText = startPos != -1 && endPos != -1 && endPos > offset
            ? docCommentXml.Substring(offset, endPos - offset)
            : noSummary;
         summaryText = Regex.Replace(summaryText, stripMisc, symbol.Name);

         return summaryText.Trim();
      }

      private sealed class SymbolEqualityComparer : IEqualityComparer<ISymbol>
      {
         public static IEqualityComparer<ISymbol> Default { get; } = new SymbolEqualityComparer();

         public bool Equals(ISymbol x, ISymbol y) => x.Name.Equals(y.Name, StringComparison.CurrentCulture);

         public int GetHashCode(ISymbol obj) => obj.Name.GetHashCode();
      }
   }
}