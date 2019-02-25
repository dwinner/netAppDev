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
      private const int InitialCapacity = 0x80;

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
               foreach (var symbolInterface in typeSymbol.Interfaces)
               {
                  typeSet.Add(symbolInterface);
               }

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
            accesibleSymbols
               .OfType<IMethodSymbol>()
               .Where(methodSymbol => methodSymbol.MethodKind == MethodKind.Ordinary),
            SymbolEqualityComparer.Default);

      public static IEnumerable<IPropertySymbol> GetOverridableProperties(IEnumerable<ISymbol> accesibleSymbols) =>
         new HashSet<IPropertySymbol>(accesibleSymbols.OfType<IPropertySymbol>(), SymbolEqualityComparer.Default);

      public static string ToSignature(this IPropertySymbol propertySymbol)
      {
         var propertyName = propertySymbol.Name;
         var propertyTypeName = propertySymbol.Type.NormalizeTypeSymbol();
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

      public static string ToSignature(this IMethodSymbol methodSymbol)
      {
         var methodName = methodSymbol.Name;
         const string separator = ", ";

         if (methodSymbol.IsGenericMethod)
         {
            var typeParams = methodSymbol.TypeParameters;
            var joinedString = Join("<", ">", separator, typeParams, symbol => symbol.Name);
            methodName += joinedString;
         }

         var returnTypeModifiers = GetTypeModifiers(methodSymbol.RefKind);
         var returnType = methodSymbol.ReturnType.GetReturnTypeToDisplay();
         if (returnTypeModifiers.Length > 0)
         {
            returnType = $"{returnTypeModifiers} {returnType}";
         }

         var accessModifiers = methodSymbol.GetAccessModifiers();
         var signature = new StringBuilder($"{accessModifiers} override ", InitialCapacity);

         signature.Append($"{returnType} {methodName}");
         var parameters = methodSymbol.Parameters;
         var parameterList = Join("(", ")", separator, parameters, symbol =>
         {
            var paramModifiers = GetTypeModifiers(symbol.RefKind);
            var parameterDisplayType = symbol.Type.GetReturnTypeToDisplay(symbol);
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

      public static string NormalizeTypeSymbol(this ITypeSymbol pointedAtType) =>
         pointedAtType.SpecialType == SpecialType.None
            ? pointedAtType.Name
            : pointedAtType.ToDisplayString();

      private static string ProcessArrayTypeKind(IParameterSymbol parameterSymbol, IArrayTypeSymbol arrayTypeSymbol)
      {
         string returnTypeName;
         var arrayElementType = arrayTypeSymbol.ElementType;
         var elementTypeDisplay = arrayElementType.NormalizeTypeSymbol();
         if (parameterSymbol?.IsParams == true) // FIXME: strange case with parameter usage here
         {
            returnTypeName = $"params {elementTypeDisplay}[]";
         }
         else
         {
            var arrayRank = arrayTypeSymbol.Rank;
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

      private static string Join<T>(string start, string end, string separator,
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

      private sealed class SymbolEqualityComparer : IEqualityComparer<ISymbol>
      {
         public static IEqualityComparer<ISymbol> Default { get; } = new SymbolEqualityComparer();

         public bool Equals(ISymbol x, ISymbol y) => x.Name.Equals(y.Name, StringComparison.CurrentCulture);

         public int GetHashCode(ISymbol obj) => obj.Name.GetHashCode();
      }
   }
}